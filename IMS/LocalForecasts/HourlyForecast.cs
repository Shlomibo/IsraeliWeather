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
		#region Fields

		private readonly Location location;
		private readonly IReadOnlyList<Forecast> forecasts; 
		#endregion

		#region Properties

		public Location Location
		{
			get { return this.location; }
		}

		public IReadOnlyList<Forecast> Forecasts
		{
			get { return this.forecasts; }
		}
		#endregion

		#region Ctor

		public HourlyForecast(
			Location location,
			IEnumerable<Forecast> forecasts)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}

			if (forecasts == null)
			{
				throw new ArgumentNullException("forecasts");
			}

			this.location = location;
			this.forecasts = new ReadOnlyCollection<Forecast>(forecasts.OrderBy(forecast => forecast.Time)
																	   .ToArray());
		}
		#endregion
	}
}
