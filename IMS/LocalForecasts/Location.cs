using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.LocalForecasts
{
	public sealed class Location
	{
		#region Properties

		public string Name { get; }

		public double Latitude { get; }

		public double Longitude { get; }

		public int Altitude { get; }
		#endregion

		#region Ctor

		public Location(
			string name,
			double latitude,
			double longitude,
			int altitude)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(nameof(name));
			}

			if ((latitude < Utilities.Units.MIN_LATITUDE) ||
				(latitude > Utilities.Units.MAX_LATITUDE))
			{
				throw new ArgumentOutOfRangeException(nameof(latitude));
			}

			if ((longitude < Utilities.Units.MIN_LONGITUDE) ||
				(longitude > Utilities.Units.MAX_LONGITUDE))
			{
				throw new ArgumentOutOfRangeException(nameof(longitude));
			}

			this.Name = name;
			this.Latitude = latitude;
			this.Longitude = longitude;
			this.Altitude = altitude;
		}
		#endregion
	}
}
