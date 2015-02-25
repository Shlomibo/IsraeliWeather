using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.GeneralForecasts
{
	public sealed class DailyForecast
	{
		#region Properties

		public DateTime Date { get; }
		public string HebrewWarning { get; }
		public string EnglishWarning { get; }
		public string HebrewForecast { get; }
		public string EnglishForecast { get; }
		#endregion

		#region Ctor

		public DailyForecast(
			DateTime date, 
			string hebrewForecast, 
			string englishForecast, 
			string hebrewWarning = null, 
			string englishWarning = null)
		{
			if (hebrewForecast == null)
			{
				throw new NullReferenceException(nameof(hebrewForecast));
			}

			if (string.IsNullOrWhiteSpace(hebrewForecast))
			{
				throw new ArgumentException(nameof(hebrewForecast));
			}

			if (englishForecast == null)
			{
				throw new NullReferenceException(nameof(englishForecast));
			}

			if (string.IsNullOrWhiteSpace(englishForecast))
			{
				throw new ArgumentException(nameof(englishForecast));
			}

			if ((hebrewWarning == null) && (englishWarning != null))
			{
				throw new ArgumentNullException(nameof(hebrewWarning));
			}

			if ((hebrewWarning != null) && (englishWarning == null))
			{
				throw new ArgumentNullException(nameof(englishWarning));
			}

			this.Date = date;
			this.HebrewForecast = hebrewForecast;
			this.EnglishForecast = englishForecast;
			this.HebrewWarning = hebrewWarning;
			this.EnglishWarning = englishWarning;
		}
		#endregion
	}
}
