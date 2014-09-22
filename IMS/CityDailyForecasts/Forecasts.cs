using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CityDailyForecasts
{
	public sealed class Forecasts
	{
		#region Fields

		private readonly DateTime issuedDate;
		private readonly ICollection<Location> locations;
		#endregion

		#region Properties

		public DateTime IssuedDate
		{
			get { return issuedDate; }
		}

		public ICollection<Location> Locations
		{
			get { return locations; }
		}
		#endregion

		#region Ctor

		public Forecasts(DateTime issuedDate, IEnumerable<Location> locations)
		{
			if (locations == null)
			{
				throw new ArgumentNullException("locations");
			}

			this.issuedDate = issuedDate;

			this.locations = new ReadOnlyCollection<Location>((from location in locations
															   where location != null
															   orderby location.Latitude descending, 
																	   location.Longitude
															   select location).ToArray());
		}
		#endregion
	}
}
