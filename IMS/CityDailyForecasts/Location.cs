using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CityDailyForecasts
{
	public sealed class Location
	{
		#region Fields

		private readonly int id;
		private readonly string nameEnglish;
		private readonly string nameHebrew;
		private readonly float latitude;
		private readonly float longitude;
		private readonly string remarksEnglish;
		private readonly string remarksHebrew;
		private readonly string remarks;
		private readonly ICollection<Forecast> forecasts;
		#endregion

		#region Properties

		public int Id
		{
			get { return this.id; }
		}

		public string NameEnglish
		{
			get { return this.nameEnglish; }
		}

		public string NameHebrew
		{
			get { return this.nameHebrew; }
		}

		public float Latitude
		{
			get { return this.latitude; }
		}

		public float Longitude
		{
			get { return this.longitude; }
		}

		public string RemarksEnglish
		{
			get { return this.remarksEnglish; }
		}

		public string RemarksHebrew
		{
			get { return this.remarksHebrew; }
		}

		public string Remarks
		{
			get { return this.remarks; }
		}
		#endregion

		#region Ctors

		public Location(
			int id,
			string nameEnglish,
			string nameHebrew,
			float latitude,
			float longitude,
			IEnumerable<Forecast> forecasts,
			string remarksEnglish = null,
			string remarksHebrew = null,
			string remarks = null)
		{
			if (nameEnglish == null)
			{
				throw new ArgumentNullException("nameEnglish");
			}

			if (nameHebrew == null)
			{
				throw new ArgumentNullException("nameHebrew");
			}

			if (forecasts == null)
			{
				throw new ArgumentNullException("forecasts");
			}

			if ((latitude < Utilities.Units.MIN_LATITUDE) ||
				(latitude > Utilities.Units.MAX_LATITUDE))
			{
				throw new ArgumentOutOfRangeException("latitude");
			}

			if ((longitude < Utilities.Units.MIN_LONGITUDE) ||
				(longitude > Utilities.Units.MAX_LONGITUDE))
			{
				throw new ArgumentOutOfRangeException("longitude");
			}

			this.id = id;
			this.nameEnglish = nameEnglish;
			this.nameHebrew = nameHebrew;
			this.latitude = latitude;
			this.longitude = longitude;
			this.remarksEnglish = remarksEnglish;
			this.remarksHebrew = remarksHebrew;
			this.remarks = remarks;

			this.forecasts = new ReadOnlyCollection<Forecast>(forecasts.Where(forecast => forecast != null)
																	   .OrderBy(forecast => forecast.Date)
																	   .ToArray());
		}
		#endregion
	}
}
