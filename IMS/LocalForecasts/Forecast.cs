using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.LocalForecasts
{
	public sealed class Forecast
	{
		#region Consts

		private const int MAX_DIRECTION = 360;
		private const int MIN_DIRECTION = 0;
		private const int MIN_PERCENTAGE = 0;
		private const int MAX_PERCENTAGE = 100;
		#endregion

		#region Properties

		public DateTime Time { get; }

		public float Temperature { get; }

		public int RelativeHumidity { get; }

		public float WindSpeed { get; }

		public int WindDirection { get; }
		#endregion

		#region Ctor

		public Forecast(
			DateTime time,
			float temperature,
			int relativeHumidity,
			float windSpeed,
			int windDirection)
		{
			if ((relativeHumidity < MIN_PERCENTAGE) || (relativeHumidity > MAX_PERCENTAGE))
			{
				throw new ArgumentOutOfRangeException(nameof(relativeHumidity));
			}

			if (windSpeed < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(windSpeed));
			}

			if ((windDirection < MIN_DIRECTION) || (windDirection > MAX_DIRECTION))
			{
				throw new ArgumentOutOfRangeException(nameof(windDirection));
			}

			this.Time = time;
			this.Temperature = temperature;
			this.RelativeHumidity = relativeHumidity;
			this.WindSpeed = windSpeed;
			this.WindDirection = windDirection;
		}
		#endregion
	}
}
