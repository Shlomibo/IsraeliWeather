using IMS.GeneralForecasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IMS
{
	public sealed class GeneralForecast : DataFile<GeneralForecasts.GeneralForecast>
	{
		#region Consts

		private const string FILE_URL = @"http://www.ims.gov.il/ims/PublicXML/isr_country.xml";
		private const string ISSUED_DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm";
		#endregion

		#region Fields

		private static readonly Uri fileUri = new Uri(FILE_URL);
		private static readonly TimeSpan refreshInterval = new TimeSpan(
			hours: 12,
			minutes: 0,
			seconds: 0);
		private XName XML_EL_IDENTIFICATION;
		private XName XML_EL_ISSUE_DATE_TIME;
		private XName XML_EL_LOCATION;
		private XName XML_EL_LOCATION_DATE;
		private XName XML_EL_TIME_UNIT_DATA;
		#endregion

		#region Ctor

		public GeneralForecast()
			: base(fileUri, refreshInterval)
		{ }
		#endregion

		#region Methods
		protected override async Task<GeneralForecasts.GeneralForecast> ParseData(XDocument fileData)
		{
			Task<IEnumerable<DailyForecast>> forecastsParsing = ParseForecasts(fileData.Root.Element(XML_EL_LOCATION)
																							.Element(XML_EL_LOCATION_DATE)
																							.Elements(XML_EL_TIME_UNIT_DATA));

			string dateString = (string)fileData.Root.Element(XML_EL_IDENTIFICATION)
													 .Element(XML_EL_ISSUE_DATE_TIME);

			DateTime issuedDate = DateTime.ParseExact(dateString, ISSUED_DATE_TIME_FORMAT, null);

			return new GeneralForecasts.GeneralForecast(issuedDate, await forecastsParsing);
		}

		private async Task<IEnumerable<DailyForecast>> ParseForecasts(IEnumerable<XElement> forecastsElements)
		{
			IEnumerable<Task<DailyForecast>> forecasts = forecastsElements.Select(element => ParseForecast(element));

			return await Task.WhenAll(forecasts);
		}

		private async Task<DailyForecast> ParseForecast(XElement element)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
