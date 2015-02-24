using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CityDailyForecasts
{
	public sealed class Forecast
	{
		#region Consts

		private const int MIN_VALID_HUMIDITY = 0;
		private const int MAX_VALID_HUMIDITY = 100;
		#endregion

		#region Properties

		public DateTime Date { get; }

		public int? MinRelativeHumidity { get; }

		public int? MaxRelativeHumidity { get; }

		public int MinTemperature { get; }

		public int MaxTemperature { get; }

		public WeatherCode WeatherCode { get; }

		public Wind? Wind { get; }

		public Forecast(
			DateTime date, 
			int minTemperature, 
			int maxTemperature, 
			WeatherCode weatherCode)
		{
			if (!Enum.IsDefined(typeof(WeatherCode), weatherCode))
			{
				throw new ArgumentException("Invalid " + nameof(weatherCode));
			}

			this.Date = date;
			this.MinTemperature = minTemperature;
			this.MaxTemperature = maxTemperature;
			this.WeatherCode = weatherCode;
		}

		public Forecast(
			DateTime date, 
			int minTemperature, 
			int maxTemperature, 
			WeatherCode weatherCode,
			int minRelativeHumidity,
			int maxRelativeHumidity,
			Wind wind)
			: this(date, minTemperature, maxTemperature, weatherCode)
		{
			if ((minRelativeHumidity < MIN_VALID_HUMIDITY) || (minRelativeHumidity > MAX_VALID_HUMIDITY))
			{
				throw new ArgumentOutOfRangeException(nameof(minRelativeHumidity));
			}

			if ((maxRelativeHumidity < MIN_VALID_HUMIDITY) || (maxRelativeHumidity > MAX_VALID_HUMIDITY))
			{
				throw new ArgumentOutOfRangeException(nameof(maxRelativeHumidity));
			}

			this.MinRelativeHumidity = minRelativeHumidity;
			this.MaxRelativeHumidity = maxRelativeHumidity;
			this.Wind = wind;
		}
		#endregion

		#region Methods

		// override object.Equals
		public override bool Equals(object obj)
		{
			//       
			// See the full list of guidelines at
			//   http://go.microsoft.com/fwlink/?LinkID=85237  
			// and also the guidance for operator== at
			//   http://go.microsoft.com/fwlink/?LinkId=85238
			//

			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			// TODO: write your implementation of Equals() here
			var other = (Forecast)obj;

			return (this.Date == other.Date) &&
				(this.MinTemperature == other.MinTemperature) &&
				(this.MaxTemperature == other.MaxTemperature) &&
				(this.WeatherCode == other.WeatherCode) &&
				(this.MinRelativeHumidity == other.MinRelativeHumidity) &&
				(this.MaxRelativeHumidity == other.MaxRelativeHumidity) &&
				(this.Wind == other.Wind);
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			return Utilities.Hash(
				this.Date,
				this.MinTemperature,
				this.MaxTemperature,
				this.WeatherCode,
				this.MinRelativeHumidity,
				this.MaxRelativeHumidity,
				this.Wind);
		}
		#endregion

		#region Operators

		public static bool operator ==(Forecast left, Forecast right)
		{
			if (object.ReferenceEquals(left, null))
			{
				return object.ReferenceEquals(right, null);
			}
			else
			{
				return left.Equals(right);
			}
		}

		public static bool operator !=(Forecast left, Forecast right) =>
			!(left == right);
		#endregion
	}
}
