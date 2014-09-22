using IMS.CityDailyForecasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IMS
{
	public sealed class CityForecasts : DataFile<Forecasts>
	{
		#region Consts

		private const string FILE_URI = @"http://www.ims.gov.il/ims/PublicXML/isr_cities.xml";
		private const string ISSUED_DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm";

		private const string XML_EL_LOCATION = "Location";
		private const string XML_EL_IDENTIFICATION = "Identification";
		private const string XML_EL_ISSUE_DATE_TIME = "IssueDateTime";
		#endregion

		#region Fields

		private static readonly Uri fileUri = new Uri(FILE_URI);
		private static readonly TimeSpan refreshInterval = new TimeSpan(
			hours: 6,
			minutes: 0,
			seconds: 0);
		#endregion

		public CityForecasts() : base(fileUri, refreshInterval) { }

		protected override async Task<Forecasts> ParseData(XDocument fileData)
		{
			var locationsTask = ParseLocations(fileData.Root.Elements(XML_EL_LOCATION));
			DateTime issuedDate = DateTime.ParseExact(
				(string)fileData.Root.Element(XML_EL_IDENTIFICATION)
									 .Element(XML_EL_ISSUE_DATE_TIME),
				ISSUED_DATE_TIME_FORMAT,
				null);

			IEnumerable<Location> locations = await locationsTask;

			return new Forecasts(issuedDate, locations);
		}

		private Task<IEnumerable<Location>> ParseLocations(IEnumerable<XElement> enumerable)
		{
			throw new NotImplementedException();
		}
	}
}
