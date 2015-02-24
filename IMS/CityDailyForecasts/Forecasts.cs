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
		#region Properties

		public DateTime IssuedDate { get; }

		public ICollection<Location> Locations { get; }
		#endregion

		#region Ctor

		public Forecasts(DateTime issuedDate, IEnumerable<Location> locations)
		{
			if (locations == null)
			{
				throw new ArgumentNullException(nameof(locations));
			}

			this.IssuedDate = issuedDate;

			this.Locations = new ReadOnlyCollection<Location>((from location in locations
															   where location != null
															   orderby location.Latitude descending, 
																	   location.Longitude
															   select location)
															  .ToArray());
		}
		#endregion
	}
}
