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

		private readonly DateTime issuedTime;
		private readonly HourlyForecast[] forecasts;
		#endregion

		#region Properties

		public DateTime IssuedTime
		{
			get { return this.issuedTime; }
		}

		public int Count
		{
			get { return this.forecasts.Length; }
		}

		bool ICollection<HourlyForecast>.IsReadOnly
		{
			get { return true; }
		}
		#endregion

		#region Ctor

		public HourlyForecasts(DateTime issuedTime, IEnumerable<HourlyForecast> forecasts)
		{
			if (forecasts == null)
			{
				throw new ArgumentNullException("forecasts");
			}

			this.issuedTime = issuedTime;
			this.forecasts = forecasts.Select(forecast =>
				{
					if (forecast == null)
					{
						throw new ArgumentException("'forecasts' cannot contain items which are null", "forecasts");
					}

					return forecast;
				}).ToArray();
		}
		#endregion

		#region Methods

		public void Add(HourlyForecast item)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(HourlyForecast item)
		{
			return this.forecasts.Contains(item);
		}

		public void CopyTo(HourlyForecast[] array, int arrayIndex)
		{
			this.forecasts.CopyTo(array, arrayIndex);
		}

		public bool Remove(HourlyForecast item)
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

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}
