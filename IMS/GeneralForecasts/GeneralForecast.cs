using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.GeneralForecasts
{
	public sealed class GeneralForecast : ICollection<DailyForecast>, IReadOnlyCollection<DailyForecast>
	{
		#region Fields

		private readonly List<DailyForecast> forecasts;
		#endregion

		#region Properties

		public int Count => this.forecasts.Count;

		bool ICollection<DailyForecast>.IsReadOnly => true;

		public DateTime IssuedDate { get; }
		#endregion

		#region Ctor

		public GeneralForecast(DateTime issuedDate, IEnumerable<DailyForecast> forecasts)
		{
			if (forecasts == null)
			{
				throw new NullReferenceException(nameof(forecasts));
			}

			this.IssuedDate = issuedDate;
			this.forecasts = forecasts.Where(forecast => forecasts != null)
									  .ToList();
		}
		#endregion

		#region Methods

		public IEnumerator<DailyForecast> GetEnumerator() =>
			this.forecasts.GetEnumerator();

		void ICollection<DailyForecast>.Add(DailyForecast item)
		{
			throw new NotSupportedException();
		}

		void ICollection<DailyForecast>.Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(DailyForecast item) =>
			this.forecasts.Contains(item);

		public void CopyTo(DailyForecast[] array, int arrayIndex) =>
			this.forecasts.CopyTo(array, arrayIndex);

		IEnumerator IEnumerable.GetEnumerator() =>
			GetEnumerator();

		bool ICollection<DailyForecast>.Remove(DailyForecast item)
		{
			throw new NotSupportedException();
		} 
		#endregion
	}
}
