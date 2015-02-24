using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMS.CityDailyForecasts
{
	public struct Wind : IEquatable<Wind>
	{
		#region Consts

		private const int MIN_VALID_SPEED = 0;
		private const int MIN_VALID_DIRECTION = 0;
		private const int MAX_VALID_DIRECTION = 360;

		private const string RGX_MIN_DIR = "mindir";
		private const string RGX_MAX_DIR = "maxdir";
		private const string RGX_MIN_SPEED = "minspd";
		private const string RGX_MAX_SPEED = "maxspd";
		#endregion

		#region Fields

		private static readonly Regex windExpression = new Regex(
			string.Format(@"^(?<{0}>\d+)(-(?<{1}>\d+))?\/(?<{2}>-?\d+)-(?<{3}>-?\d+)$",
				RGX_MIN_DIR,
				RGX_MAX_DIR,
				RGX_MIN_SPEED,
				RGX_MAX_SPEED),
			RegexOptions.ExplicitCapture);
		#endregion

		#region Properties

		public int MinDirection { get; }

		public int MaxDirection { get; }

		public int MinSpeed { get; }

		public int MaxSpeed { get; }
		#endregion

		#region Ctor

		public Wind(int minSpeed, int maxSpeed, int direction, int? maxDirection = null)
			: this()
		{
			if (minSpeed < MIN_VALID_SPEED)
			{
				throw new ArgumentOutOfRangeException(nameof(minSpeed));
			}

			if (maxSpeed < MIN_VALID_SPEED)
			{
				throw new ArgumentOutOfRangeException(nameof(maxSpeed));
			}

			if (maxSpeed < minSpeed)
			{
				throw new ArgumentException($"{nameof(minSpeed)} cannot be greater than {nameof(maxSpeed)}");
			}

			if ((direction < MIN_VALID_DIRECTION) || (direction > MAX_VALID_DIRECTION))
			{
				throw new ArgumentOutOfRangeException(nameof(direction));
			}

			if (maxDirection.HasValue &&
				((maxDirection < MIN_VALID_DIRECTION) || (maxDirection > MAX_VALID_DIRECTION)))
			{
				throw new ArgumentOutOfRangeException(nameof(maxDirection));
			}

			if (maxDirection.HasValue && (maxDirection > direction))
			{
				throw new ArgumentException($"{nameof(direction)} cannot be greater than {nameof(maxDirection)}");
			}

			this.MinSpeed = minSpeed;
			this.MaxSpeed = maxSpeed;
			this.MinDirection = direction;
			this.MaxDirection = maxDirection ?? direction;
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
			return Equals((Wind)obj);
		}

		public override int GetHashCode() =>
			Utilities.Hash(
				this.MinSpeed,
				this.MaxSpeed,
				this.MinDirection,
				this.MaxDirection);

		public bool Equals(Wind other) =>
			(this.MinSpeed == other.MinSpeed) &&
			(this.MaxSpeed == other.MaxSpeed) &&
			(this.MinDirection == other.MinDirection) &&
			(this.MaxDirection == other.MaxDirection);

		public override string ToString()
		{
			var builder = new StringBuilder(this.MinDirection);

			if (this.MaxDirection != this.MinDirection)
			{
				builder.AppendFormat("-{0}", this.MaxDirection);
			}

			builder.AppendFormat("/{0}-{1}", this.MinSpeed, this.MaxSpeed);

			return builder.ToString();
		}

		public static bool TryParse(string s, out Wind result)
		{
			result = default(Wind);
			bool didSucceed = false;

			if (s != null)
			{
				var match = windExpression.Match(s);

				if (match.Success)
				{
					int minDirection = int.Parse(match.Groups[RGX_MIN_DIR].Value);
					int maxDirection = match.Groups[RGX_MAX_DIR].Success
						? int.Parse(match.Groups[RGX_MAX_DIR].Value)
						: minDirection;
					int minSpeed = int.Parse(match.Groups[RGX_MIN_SPEED].Value);
					int maxSpeed = int.Parse(match.Groups[RGX_MAX_SPEED].Value);

					try
					{
						result = new Wind(
							minSpeed,
							maxSpeed,
							minDirection,
							maxDirection);
						didSucceed = true;
					}
					catch { }
				}
			}

			return didSucceed;
		}

		public static Wind Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException(nameof(s));
			}

			Wind wind;

			if (!TryParse(s, out wind))
			{
				throw new FormatException("The string was in an invalid format");
			}

			return wind;
		}
		#endregion

		#region Operator

		public static bool operator ==(Wind left, Wind right) =>
			left.Equals(right);

		public static bool operator !=(Wind left, Wind right) =>
			!(left == right);
		#endregion
	}
}
