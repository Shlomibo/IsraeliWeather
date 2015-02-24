using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.LocalForecasts
{
	public sealed class HourlyForecast 
	{
		#region Properties

		public Location Location { get; }

		public IReadOnlyList<Forecast> Forecasts { get; }
		#endregion

		#region Ctor

		public HourlyForecast(
			Location location,
			IEnumerable<Forecast> forecasts)
		{
			if (location == null)
			{
				throw new ArgumentNullException(nameof(location));
			}

			if (forecasts == null)
			{
				throw new ArgumentNullException(nameof(forecasts));
			}

			this.Location = location;
			this.Forecasts = new ReadOnlyCollection<Forecast>(forecasts.OrderBy(forecast => forecast.Time)
																	   .ToArray());
		}
		#endregion
	}
}
