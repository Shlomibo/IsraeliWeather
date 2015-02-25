using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
		#endregion

		#region Ctors

		public Forecast(ForecastBuilder builder)
		{
			this.Date = builder.Date;
			this.MinTemperature = builder.MinTemperature;
			this.MaxTemperature = builder.MaxTemperature;
			this.WeatherCode = builder.WeatherCode;

			var nullables = new object[] 
				{
					builder.MinRelativeHumidity,
					builder.MaxRelativeHumidity,
					builder.Wind
				};

			if (nullables.Any(nullable => nullable == null) && 
				!nullables.All(nullable => nullable == null))
			{
				throw new InvalidOperationException("All relative humidity values must be set.");
			}

			if ((builder.MinRelativeHumidity < MIN_VALID_HUMIDITY) || (builder.MinRelativeHumidity > MAX_VALID_HUMIDITY))
			{
				throw new ArgumentOutOfRangeException(nameof(ForecastBuilder.MinRelativeHumidity));
			}

			if ((builder.MaxRelativeHumidity < MIN_VALID_HUMIDITY) || (builder.MaxRelativeHumidity > MAX_VALID_HUMIDITY))
			{
				throw new ArgumentOutOfRangeException(nameof(ForecastBuilder.MaxRelativeHumidity));
			}

			this.MinRelativeHumidity = builder.MinRelativeHumidity;
			this.MaxRelativeHumidity = builder.MaxRelativeHumidity;
			this.Wind = builder.Wind;
		}

		public Forecast(
			DateTime date,
			int minTemperature,
			int maxTemperature,
			WeatherCode weatherCode)
			: this(new ForecastBuilder
			{
				Date = date,
				MinTemperature = minTemperature,
				MaxTemperature = maxTemperature,
				WeatherCode = weatherCode,
			})
		{ }

		public Forecast(
			DateTime date,
			int minTemperature,
			int maxTemperature,
			WeatherCode weatherCode,
			int minRelativeHumidity,
			int maxRelativeHumidity,
			Wind wind)
			: this(new ForecastBuilder
			{
				Date = date,
				MinTemperature = minTemperature,
				MaxTemperature = maxTemperature,
				WeatherCode = weatherCode,
				MinRelativeHumidity = minRelativeHumidity,
				MaxRelativeHumidity = maxRelativeHumidity,
				Wind = wind,
			})
		{ }
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

		public sealed class ForecastBuilder
		{
			#region Consts

		private const string FORECAST_DATE_FORMAT = "yyyy-MM-dd";
			#endregion
	
			#region Fields

			private static readonly Dictionary<string, Func<string, ForecastBuilder, bool>> setterByPropertyName =
				new Dictionary<string, Func<string, ForecastBuilder, bool>>
				{
					{ nameof(Date), SetDate },
					{ nameof(MinRelativeHumidity), SetMinHumidity },
					{ nameof(MaxRelativeHumidity), SetMaxHumidity },
					{ nameof(MinTemperature), SetMinTemperature },
					{ nameof(MaxTemperature), SetMaxTemperature },
					{ nameof(WeatherCode), SetWeatherCode },
					{ nameof(Wind), SetWind },
				};

			private WeatherCode weatherCode; 
			#endregion

			#region Properties

			public DateTime Date { get; set; }

			public int? MinRelativeHumidity { get; set; }

			public int? MaxRelativeHumidity { get; set; }

			public int MinTemperature { get; set; }

			public int MaxTemperature { get; set; }

			public WeatherCode WeatherCode
			{
				get { return this.weatherCode; }
				set
				{
					if (!Enum.IsDefined(typeof(WeatherCode), value))
					{
						throw new ArgumentException(nameof(this.WeatherCode));
					}
				}
			}

			public Wind? Wind { get; set; }
			#endregion

			#region Ctors

			public ForecastBuilder() { }

			public ForecastBuilder(IDictionary<string, string> forecastProperties)
			{
				if (forecastProperties == null)
				{
					throw new NullReferenceException(nameof(forecastProperties));
				}

				foreach (string key in forecastProperties.Keys)
				{
					Func<string, ForecastBuilder, bool> setter;

					if (!setterByPropertyName.TryGetValue(key, out setter))
					{
						throw new ArgumentOutOfRangeException($"Invalid property '{key}'");
					}
					else if (!setter(forecastProperties[key], this))
					{
						throw new FormatException($"Could not parse the property '{key}', with value '{forecastProperties[key]}'");
					}
				}
			}
			#endregion

			#region Methods

			private static bool SetWind(string windString, ForecastBuilder builder)
			{
				Wind wind;
				bool result = false;

				if (string.IsNullOrWhiteSpace(windString))
				{
					result = true;
					builder.Wind = null;
				}
				else
				{
					result = CityDailyForecasts.Wind.TryParse(windString, out wind);

					if (result)
					{
						builder.Wind = wind;
					}
				}

				return result;
			}

			private static bool SetWeatherCode(string weatherCodeString, ForecastBuilder builder)
			{
				int weatherCodeValue;
				bool result = int.TryParse(weatherCodeString, out weatherCodeValue);

				if (result)
				{
					result &= Enum.IsDefined(typeof(WeatherCode), weatherCodeValue);

					if (result)
					{
						builder.WeatherCode = (WeatherCode)weatherCodeValue;
					}
				}

				return result;
			}

			private static bool SetMaxTemperature(string tempString, ForecastBuilder builder)
			{
				int temp;
				bool result = int.TryParse(tempString, out temp);

				if (result)
				{
					builder.MaxTemperature = temp;
				}

				return result;
			}

			private static bool SetMinTemperature(string tempString, ForecastBuilder builder)
			{
				int temp;
				bool result = int.TryParse(tempString, out temp);

				if (result)
				{
					builder.MinTemperature = temp;
				}

				return result;
			}

			private static bool SetMaxHumidity(string humidityString, ForecastBuilder builder)
			{
				int? humidity;
				bool result = GetHumidity(humidityString, out humidity);

				if (result)
				{
					builder.MaxRelativeHumidity = humidity;
				}

				return result;
			}

			private static bool SetMinHumidity(string humidityString, ForecastBuilder builder)
			{
				int? humidity;
				bool result = GetHumidity(humidityString, out humidity);

				if (result)
				{
					builder.MinRelativeHumidity = humidity;
				}

				return result;
			}

			private static bool GetHumidity(string humidityString, out int? humidity)
			{
				bool result = false;
				humidity = null;

				if (string.IsNullOrWhiteSpace(humidityString))
				{
					result = true;
				}
				else
				{
					int value;
					result = int.TryParse(humidityString, out value);

					if (result)
					{
						humidity = value;
					}
				}

				return result;
			}

			private static bool SetDate(string dateString, ForecastBuilder builder)
			{
				Debug.Assert(builder != null, nameof(builder) + " is null.");

				bool result = false;
				DateTime date;

				result = DateTime.TryParseExact(dateString,
					FORECAST_DATE_FORMAT,
					null,
					DateTimeStyles.AssumeLocal,
					out date);

				if (result)
				{
					builder.Date = date;
				}

				return result;
			}
			#endregion
		}
	}
}
