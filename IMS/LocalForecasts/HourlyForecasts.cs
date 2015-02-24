using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.LocalForecasts
{
	public sealed class HourlyForecasts : ICollection<HourlyForecast>, IReadOnlyCollection<HourlyForecast>
	{
		#region Fields

		private readonly HourlyForecast[] forecasts;
		#endregion

		#region Properties

		public DateTime IssuedTime { get; }

		public int Count => this.forecasts.Length;

		bool ICollection<HourlyForecast>.IsReadOnly => true;
		#endregion

		#region Ctor

		public HourlyForecasts(DateTime issuedTime, IEnumerable<HourlyForecast> forecasts)
		{
			if (forecasts == null)
			{
				throw new ArgumentNullException(nameof(forecasts));
			}

			this.IssuedTime = issuedTime;
			this.forecasts = forecasts.Select(forecast =>
				{
					if (forecast == null)
					{
						throw new ArgumentException(
							$"'{nameof(forecasts)}' cannot contain items which are null", 
							nameof(forecasts));
					}

					return forecast;
				}).ToArray();
		}
		#endregion

		#region Methods

		void ICollection<HourlyForecast>.Add(HourlyForecast item)
		{
			throw new NotSupportedException();
		}

		void ICollection<HourlyForecast>.Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(HourlyForecast item) =>
			this.forecasts.Contains(item);

		public void CopyTo(HourlyForecast[] array, int arrayIndex) =>
			this.forecasts.CopyTo(array, arrayIndex);

		bool ICollection<HourlyForecast>.Remove(HourlyForecast item)
		{
			throw new NotSupportedException();
		}

		public IEnumerator<HourlyForecast> GetEnumerator()
		{
			foreach (HourlyForecast forecast in this.forecasts)
			{
				yield return forecast;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() =>
			GetEnumerator();
		#endregion
	}
}
