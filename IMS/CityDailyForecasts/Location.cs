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
		#region Properties

		public int Id { get; }

		public string NameEnglish { get; }

		public string NameHebrew { get; }

		public float Latitude { get; }

		public float Longitude { get; }

		public string RemarksEnglish { get; }

		public string RemarksHebrew { get; }

		public string Remarks { get; }

		public ICollection<Forecast> Forecasts { get; }
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
				throw new ArgumentNullException(nameof(nameEnglish));
			}

			if (nameHebrew == null)
			{
				throw new ArgumentNullException(nameof(nameHebrew));
			}

			if (forecasts == null)
			{
				throw new ArgumentNullException(nameof(forecasts));
			}

			if ((latitude < Utilities.Units.MIN_LATITUDE) ||
				(latitude > Utilities.Units.MAX_LATITUDE))
			{
				throw new ArgumentOutOfRangeException(nameof(latitude));
			}

			if ((longitude < Utilities.Units.MIN_LONGITUDE) ||
				(longitude > Utilities.Units.MAX_LONGITUDE))
			{
				throw new ArgumentOutOfRangeException(nameof(longitude));
			}

			this.Id = id;
			this.NameEnglish = nameEnglish;
			this.NameHebrew = nameHebrew;
			this.Latitude = latitude;
			this.Longitude = longitude;
			this.RemarksEnglish = remarksEnglish;
			this.RemarksHebrew = remarksHebrew;
			this.Remarks = remarks;

			this.Forecasts = new ReadOnlyCollection<Forecast>(forecasts.Where(forecast => forecast != null)
																	   .OrderBy(forecast => forecast.Date)
																	   .ToArray());
		}
		#endregion
	}
}
