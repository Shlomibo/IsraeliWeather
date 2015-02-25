using IMS.LocalForecasts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IMS
{
	public sealed class HourlyLocalForecast : DataFile<HourlyForecasts>
	{
		#region Consts

		private const string URL = "http://www.ims.gov.il/ims/PublicXML/IMS_001.xml";
		private const string FORECAST_TIME_FORMAT = "dd/MM/yyyy HH:mm 'UTC'";

		private const int TIME_ZONE_OFFSET = 2;
		private const int DAYLIGHT_ZIME_ZONE_OFFSET = TIME_ZONE_OFFSET + 1;
		private const string DAYLIGHT_TIME_SPECIFIER = "IDT";
		
		private const string XML_EL_IDENTIFICATION = "Identification";
		private const string XML_EL_ISSUE_DATE_TIME = "IssueDateTime";
		
		private const string XML_EL_LOCATION = "Location";
		private const string XML_EL_LOCATION_META_DATA = "LocationMetaData";
		private const string XML_EL_LOCATION_NAME = "LocationName";
		private const string XML_EL_LOCATION_LATITUDE = "LocationLatitude";
		private const string XML_EL_LOCATION_LONGITUDE = "LocationLongitude";
		private const string XML_EL_LOCATION_HEIGHT = "LocationHeight";
		private const string XML_EL_LOCATION_DATA = "LocationData";

		private const string XML_EL_FORECAST = "Forecast";
		private const string XML_EL_FORECAST_TIME = "ForecastTime";
		private const string XML_EL_TEMPERATURE = "Temperature";
		private const string XML_EL_RELATIVE_HUMIDITY = "RelativeHumidity";
		private const string XML_EL_WIND_SPEED = "WindSpeed";
		private const string XML_EL_WIND_DIRECTION = "WindDirection";
		#endregion

		#region Fields

		private static readonly Regex timeRegex = new Regex(Utilities.DateTimePattern);
		private static readonly Uri fileUrl = new Uri(URL);
		private static readonly TimeSpan refreshInterval = new TimeSpan(
			hours: 1,
			minutes: 0,
			seconds: 0);
		#endregion

		#region Ctor

		public HourlyLocalForecast() : base(fileUrl, refreshInterval) { } 
		#endregion

		protected override async Task<HourlyForecasts> ParseData(XDocument fileData)
		{
			Debug.Assert(fileData != null, nameof(fileData) + " is null.");

			DateTime issuedDate = await ParseDate(fileData.Root
														  .Element(XML_EL_IDENTIFICATION)
														  .Element(XML_EL_ISSUE_DATE_TIME)
														  .Value);


			IEnumerable<HourlyForecast> forecasts = await ParseAllForecasts(fileData.Root
																					.Elements(XML_EL_LOCATION));

			return new HourlyForecasts(issuedDate, forecasts);
		}

		private async Task<IEnumerable<HourlyForecast>> ParseAllForecasts(IEnumerable<XElement> locations)
		{
			Debug.Assert(locations != null, nameof(locations) + " is null.");

			IEnumerable<Task<HourlyForecast>> locationsData = locations.Select(location => ParseLocation(location));

			await Task.WhenAll(locationsData);

			return locationsData.Select(locationTask => locationTask.Result);
		}

		private async Task<HourlyForecast> ParseLocation(XElement location)
		{
			Debug.Assert(location != null, nameof(location) + " is null.");

			XElement locationData = location.Element(XML_EL_LOCATION_META_DATA);
			
			var name = (string)locationData.Element(XML_EL_LOCATION_NAME);
			var latitude = (double)locationData.Element(XML_EL_LOCATION_LATITUDE);
			var longitude = (double)locationData.Element(XML_EL_LOCATION_LONGITUDE);
			var altitude = (int)locationData.Element(XML_EL_LOCATION_HEIGHT);

			IEnumerable<Forecast> forecasts = await ParseLocationForecasts(location.Element(XML_EL_LOCATION_DATA)
																				   .Elements(XML_EL_FORECAST));

			return new HourlyForecast(
				forecasts: forecasts,
				location: new LocalForecasts.Location(
					name,
					latitude,
					longitude,
					altitude));
		}

		private async Task<IEnumerable<Forecast>> ParseLocationForecasts(IEnumerable<XElement> forecasts)
		{
			Debug.Assert(forecasts != null, nameof(forecasts) + " is null.");

			IEnumerable<Task<Forecast>> forecastsTasks = forecasts.Select(forecast => ParseForecast(forecast));
			await Task.WhenAll(forecastsTasks);

			return forecastsTasks.Select(task => task.Result);
		}

		private Task<Forecast> ParseForecast(XElement forecast)
		{
			return Task.Run(() =>
				{
					Debug.Assert(forecast != null, nameof(forecast) + " is null.");

					var time = DateTime.ParseExact(
						(string)forecast.Element(XML_EL_FORECAST_TIME),
						FORECAST_TIME_FORMAT,
						null);
					time = DateTime.SpecifyKind(time, DateTimeKind.Utc);

					var temperature = (float)forecast.Element(XML_EL_TEMPERATURE);
					var humidity = (int)forecast.Element(XML_EL_RELATIVE_HUMIDITY);
					var windSpeed = (float)forecast.Element(XML_EL_WIND_SPEED);
					var windDirection = (int)forecast.Element(XML_EL_WIND_DIRECTION);

					return new Forecast(
						time,
						temperature,
						humidity,
						windSpeed,
						windDirection);
				});
		}

		private Task<DateTime> ParseDate(string dateString)
		{
			return Task.Run(() =>
				{
					Match match = timeRegex.Match(dateString);

					if (!match.Success)
					{
						throw new FormatException("dateString is in the wrong format");
					}
					
					var month = (MonthDesignators)Enum.Parse(
						typeof(MonthDesignators),
						match.Groups[DateTimePatternKeys.Mon.ToString()].Value);

					var day = int.Parse(match.Groups[DateTimePatternKeys.Day.ToString()].Value);
					var hour = int.Parse(match.Groups[DateTimePatternKeys.Hour.ToString()].Value);
					var minute = int.Parse(match.Groups[DateTimePatternKeys.Min.ToString()].Value);
					var second = int.Parse(match.Groups[DateTimePatternKeys.Sec.ToString()].Value);
					var timezone = match.Groups[DateTimePatternKeys.TZone.ToString()].Value;
					var year = int.Parse(match.Groups[DateTimePatternKeys.Year.ToString()].Value);

					var date = new DateTime(year, (int)month, day, hour, minute, second);
					var offset = timezone == DAYLIGHT_TIME_SPECIFIER
						? new TimeSpan(DAYLIGHT_ZIME_ZONE_OFFSET, 0, 0)
						: new TimeSpan(TIME_ZONE_OFFSET);
					date -= offset;
					date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

					return date;
				});
		}
	}
}
