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

		#region Fields

		private readonly DateTime time;
		private readonly float temperature;
		private readonly int realtiveHumidity;
		private readonly float windSpeed;
		private readonly int windDirection;
		#endregion

		#region Properties

		public DateTime Time
		{
			get { return this.time; }
		}

		public float Temperature
		{
			get { return this.temperature; }
		}

		public int RelativeHumidity
		{
			get { return this.realtiveHumidity; }
		}

		public float WindSpeed
		{
			get { return this.windSpeed; }
		}

		public int WindDirection
		{
			get { return this.windDirection; }
		}
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
				throw new ArgumentOutOfRangeException("relativeHumidity");
			}

			if (windSpeed < 0)
			{
				throw new ArgumentOutOfRangeException("windSpeed");
			}

			if ((windDirection < MIN_DIRECTION) || (windDirection > MAX_DIRECTION))
			{
				throw new ArgumentOutOfRangeException("windDirection");
			}

			this.time = time;
			this.temperature = temperature;
			this.realtiveHumidity = relativeHumidity;
			this.windSpeed = windSpeed;
			this.windDirection = windDirection;
		}
		#endregion
	}
}
