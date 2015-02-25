using IMS.CityDailyForecasts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using System.Dynamic;

namespace IMS
{
	public sealed class CityForecasts : DataFile<Forecasts>
	{
		#region Consts

		private const string FILE_URI = @"http://www.ims.gov.il/ims/PublicXML/isr_cities.xml";
		private const string ISSUED_DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm";
		private const string FORECAST_DATE_FORMAT = "yyyy-MM-dd";

		private const string XML_EL_DATE = "Date";
		private const string XML_EL_DISPLAY_LAT = "DisplayLat";
		private const string XML_EL_DISPLAY_LON = "DisplayLon";
		private const string XML_EL_ELEMENT = "Element";
		private const string XML_EL_ELEMENT_NAME = "ElementName";
		private const string XML_EL_ELEMENT_VALUE = "ElementValue";
		private const string XML_EL_IDENTIFICATION = "Identification";
		private const string XML_EL_ISSUE_DATE_TIME = "IssueDateTime";
		private const string XML_EL_LOCATION = "Location";
		private const string XML_EL_LOCATION_DATA = "LocationData";
		private const string XML_EL_LOCATION_ID = "LocationId";
		private const string XML_EL_LOCATION_META_DATA = "LocationMetaData";
		private const string XML_EL_LOCATION_NAME_ENG = "LocationNameEng";
		private const string XML_EL_LOCATION_NAME_HEB = "LocationNameHeb";
		private const string XML_EL_REMARKS = "Remarks";
		private const string XML_EL_REMARKS_ENG = "RemarksEng";
		private const string XML_EL_REMARKS_HEB = "RemarksHeb";

		private const string XML_VAL_MAXIMUM_RELATIVE_HUMIDITY = "Maximum relative humidity";
		private const string XML_VAL_MAXIMUM_TEMPERATURE = "Maximum temperature";
		private const string XML_VAL_MINIMUM_RELATIVE_HUMIDITY = "Minimum relative humidity";
		private const string XML_VAL_MINIMUM_TEMPERATURE = "Minimum temperature";
		private const string XML_VAL_WEATHER_CODE = "Weather code";
		private const string XML_VAL_WIND_DIRECTION_AND_SPEED = "Wind direction and speed";
		#endregion

		#region Fields

		private static readonly Dictionary<string, string> forecastPropertiesByXMLProperties =
			new Dictionary<string, string>
			{
				{ XML_VAL_MINIMUM_RELATIVE_HUMIDITY, nameof(Forecast.MinRelativeHumidity) },
				{ XML_VAL_MAXIMUM_RELATIVE_HUMIDITY, nameof(Forecast.MaxRelativeHumidity) },
				{ XML_VAL_MINIMUM_TEMPERATURE, nameof(Forecast.MinTemperature) },
				{ XML_VAL_MAXIMUM_TEMPERATURE, nameof(Forecast.MaxTemperature) },
				{ XML_VAL_WEATHER_CODE, nameof(Forecast.WeatherCode) },
				{ XML_VAL_WIND_DIRECTION_AND_SPEED, nameof(Forecast.Wind) }
			};

		private static readonly Uri fileUri = new Uri(FILE_URI);
		private static readonly TimeSpan refreshInterval = new TimeSpan(
			hours: 6,
			minutes: 0,
			seconds: 0);
		#endregion

		public CityForecasts() : base(fileUri, refreshInterval) { }

		protected override async Task<Forecasts> ParseData(XDocument fileData)
		{
			Debug.Assert(fileData != null, nameof(fileData) + " is null.");

			var locationsTask = ParseLocations(fileData.Root.Elements(XML_EL_LOCATION));
			DateTime issuedDate = DateTime.ParseExact(
				(string)fileData.Root.Element(XML_EL_IDENTIFICATION)
									 .Element(XML_EL_ISSUE_DATE_TIME),
				ISSUED_DATE_TIME_FORMAT,
				null);

			IEnumerable<Location> locations = await locationsTask;

			return new Forecasts(issuedDate, locations);
		}

		private async Task<IEnumerable<Location>> ParseLocations(IEnumerable<XElement> locationElements)
		{
			Debug.Assert(locationElements != null, nameof(locationElements) + " is null.");

			IEnumerable<Task<Location>> locations = locationElements.Select(element => ParseLocation(element));

			return await Task.WhenAll(locations);
		}

		private async Task<Location> ParseLocation(XElement locationElement)
		{
			Debug.Assert(locationElement != null, nameof(locationElement) + " is null.");

			Task<IEnumerable<Forecast>> forecasts = ParseForecasts(locationElement.Element(XML_EL_LOCATION_DATA)
																				  .Elements());

			XElement metaData = locationElement.Element(XML_EL_LOCATION_META_DATA);

			int locationId = (int)metaData.Element(XML_EL_LOCATION_ID);
			string englishName = (string)metaData.Element(XML_EL_LOCATION_NAME_ENG);
			string hebrewName = (string)metaData.Element(XML_EL_LOCATION_NAME_HEB);
			float latitude = (float)metaData.Element(XML_EL_DISPLAY_LAT);
			float longitude = (float)metaData.Element(XML_EL_DISPLAY_LON);
			string englishRemarks = (string)metaData.Element(XML_EL_REMARKS_ENG);
			string hebrewRemarks = (string)metaData.Element(XML_EL_REMARKS_HEB);
			string remarks = (string)metaData.Element(XML_EL_REMARKS);

			return new Location(
				locationId,
				englishName,
				hebrewName,
				latitude,
				longitude,
				await forecasts,
				englishRemarks,
				hebrewRemarks,
				remarks);
		}

		private async Task<IEnumerable<Forecast>> ParseForecasts(IEnumerable<XElement> forecastElements)
		{
			IEnumerable<Task<Forecast>> forecastTasks = forecastElements.Select(element => ParseForecast(element));

			return await Task.WhenAll(forecastTasks);
		}

		private Task<Forecast> ParseForecast(XElement forecastElement)
		{
			return Task.Run(() =>
			{
				DateTime date = DateTime.ParseExact(
					(string)forecastElement.Element(XML_EL_DATE),
					FORECAST_DATE_FORMAT,
					null);

				var forecastProperties = forecastElement.Elements(XML_EL_ELEMENT)
														.Select(element =>
															new
															{
																Name = (string)element.Element(XML_EL_ELEMENT_NAME),
																Value = (string)element.Element(XML_EL_ELEMENT_VALUE)
															})
														.ToDictionary(
															property => forecastPropertiesByXMLProperties[property.Name],
															property => property.Value);

				var builder = new Forecast.ForecastBuilder(forecastProperties)
				{
					Date = date,
				};

				return new Forecast(builder);
			});
		}
	}
}
