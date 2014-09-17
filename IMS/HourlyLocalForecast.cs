using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS
{
	public sealed class HourlyLocalForecast : DataFile
	{
		#region Consts

		private const string URL = "http://www.ims.gov.il/ims/PublicXML/IMS_001.xml";
		#endregion

		#region Fields

		private static readonly Uri fileUrl = new Uri(URL);
		private static readonly TimeSpan refreshInterval = new TimeSpan(
			hours: 1,
			minutes: 0,
			seconds: 0);
		#endregion

		#region Ctor

		public HourlyLocalForecast() : base(fileUrl, refreshInterval) { } 
		#endregion
	}
}
