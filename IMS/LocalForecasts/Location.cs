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
		#region Consts

		private const double MIN_LATITUDE = -90;
		private const double MAX_LATITUDE = 90;
		private const double MIN_LONGITUDE = -180;
		private const double MAX_LONGITUDE = 180;
		#endregion

		#region Fields

		private readonly string name;
		private readonly double latitude;
		private readonly double longitude;
		private readonly int altitude;
		#endregion

		#region Properties

		public string Name
		{
			get { return this.name; }
		}

		public double Latitude
		{
			get { return this.latitude; }
		}

		public double Longitude
		{
			get { return this.longitude; }
		}

		public int Altitude
		{
			get { return this.altitude; }
		}
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
				throw new ArgumentNullException("name");
			}

			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name");
			}

			if ((latitude < MIN_LATITUDE) || (latitude > MAX_LATITUDE))
			{
				throw new ArgumentOutOfRangeException("latitude");
			}

			if ((longitude < MIN_LONGITUDE) || (longitude > MAX_LONGITUDE))
			{
				throw new ArgumentOutOfRangeException("longitude");
			}

			this.name = name;
			this.latitude = latitude;
			this.longitude = longitude;
			this.altitude = altitude;
		}
		#endregion
	}
}
