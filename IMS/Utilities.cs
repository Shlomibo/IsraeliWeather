using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS
{
	internal static class Utilities
	{
		private static Lazy<string> dateTimePattern = new Lazy<string>(() => CreateDateTimePattern(), true);

		public static int Hash(IEnumerable<object> objects)
		{
			const int initial = 29;
			const int shift = 6;

			return objects.Select(obj => obj == null
										 ? 0
										 : obj.GetHashCode())
						 .Aggregate(
							 initial, 
							 (hash, objHash) => (hash << shift) - hash + objHash);
		}

		public static int Hash(params object[] objects)
		{
			return Hash((IEnumerable<object>)objects);
		}

		public static string DateTimePattern
		{
			get { return dateTimePattern.Value; }
		}

		private static string CreateDateTimePattern()
		{
			var pattern = new StringBuilder("(?:");

			// Pattern for the day of the week
			pattern.Append(string.Join("|", (DayOfTheWeek[])Enum.GetValues(typeof(DayOfTheWeek))))
				   .Append(")");

			pattern.Append(" ");

			// Pattern for the month
			pattern.AppendFormat("(?<{0}>", DateTimePatternKeys.Mon)
				   .Append(string.Join("|", (MonthDesignators[])Enum.GetValues(typeof(MonthDesignators))))
				   .Append(")");

			pattern.Append(" ");

			// Pattern for the day
			pattern.AppendFormat("(?<{0}>", DateTimePatternKeys.Day)
				   .Append(@"\d{1,2})");

			pattern.Append(" ");

			// Pattern for the hour
			pattern.AppendFormat("(?<{0}>", DateTimePatternKeys.Hour)
				   .Append(@"\d{1,2})");

			pattern.Append(@"\:");

			// Pattern for the minutes
			pattern.AppendFormat("(?<{0}>", DateTimePatternKeys.Min)
				   .Append(@"\d{2})");

			pattern.Append(@"\:");

			// Pattern for the seconds
			pattern.AppendFormat(@"\:(?<{0}>", DateTimePatternKeys.Sec)
				   .Append(@"\d{2})");

			pattern.Append(" ");

			// Pattern for time-zone
			pattern.AppendFormat("(?<{0}>", DateTimePatternKeys.TZone)
				   .Append("[A-Z]{3})");

			pattern.Append(" ");
			
			// Pattern for the year
			pattern.AppendFormat(@"(?<{0}>", DateTimePatternKeys.Year)
				   .Append(@"\d{4})");

			return pattern.ToString();
		}

		public static class Units
		{
			#region Consts

			public const double MIN_LATITUDE = -90;
			public const double MAX_LATITUDE = 90;
			public const double MIN_LONGITUDE = -180;
			public const double MAX_LONGITUDE = 180;
			#endregion
		}

		private enum DayOfTheWeek
		{
			Sun,
			Mon,
			Tue,
			Wed,
			Thu,
			Fri,
			Sat,
		}
	}

	internal enum DateTimePatternKeys
	{
		Mon,
		Day,
		Hour,
		Min,
		Sec,
		Year,
		TZone,
	}

	internal enum MonthDesignators
	{
		Jan = 1,
		Feb,
		Mar,
		Apr,
		May,
		Jun,
		Jul,
		Aug,
		Sep,
		Oct,
		Nov,
		Dec,
	}
}
