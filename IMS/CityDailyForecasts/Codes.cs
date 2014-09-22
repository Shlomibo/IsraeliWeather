namespace IMS.CityDailyForecasts
{
	public enum WeatherCode
	{
		/// <summary>No code, or invalid code.</summary>
		None,
		/// <summary>Sandstorm.</summary>
		SandStorms = 1010, // סופות חול
		/// <summary>Thunderstorms.</summary>
		Thunderstorms = 1020, // סופות רעמים וברקים
		/// <summary>Hail.</summary>
		Hail = 1030, // ברד
		/// <summary>Blizzard.</summary>
		Blizzard = 1040, // סופת שלגים
		/// <summary>Snow showers.</summary>
		SnowShowers = 1050, // תזזיות שלג
		/// <summary>Snow.</summary>
		Snow = 1060, // שלג
		/// <summary>Light snow.</summary>
		LightSnow = 1070, // שלג קל
		/// <summary>Sleet.</summary>
		Sleet = 1080, // גשם מעורב בשלג
		/// <summary>Showers.</summary>
		Showers = 1090, // ממטרים
		/// <summary>Occasional showers.</summary>
		OccasionalShowers = 1100, // ממטרים פזורים
		/// <summary>Isolated showers.</summary>
		IsolatedShowers = 1110, // ממטרים מקומיים
		/// <summary>Light showers.</summary>
		LightShowers = 1120, // ממטרים קלים
		/// <summary>Freezing rain.</summary>
		FreezingRain = 1130, // גשם קופא
		/// <summary>Rain.</summary>
		Rain = 1140, // גשם
		/// <summary>Drizzle.</summary>
		Drizzle = 1150, // רסס
		/// <summary>Fog.</summary>
		Fog = 1160, // ערפל
		/// <summary>Mist.</summary>
		Mist = 1170, // ערפל קל
		/// <summary>Smoke.</summary>
		Smoke = 1180, // עשן
		/// <summary>Haze.</summary>
		Haze = 1190, // אובך
		/// <summary>Overcast.</summary>
		Overcast = 1200, // מעונן
		/// <summary>Mostly cloudy.</summary>
		MostlyCloudy = 1210, // בדרך כלל מעונן
		/// <summary>Partly cloudy.</summary>
		PartlyCloudy = 1220, // מעונן חלקית
		/// <summary>Cloudy.</summary>
		Cloudy = 1230, // מעונן
		/// <summary>Fair.</summary>
		Fair = 1240, // נאה
		/// <summary>Clear.</summary>
		Clear = 1250, // בהיר
		/// <summary>Windy.</summary>
		Windy = 1260, // רוחות ערות
		/// <summary>Wet, humid.</summary>
		Humid = 1270, // לח
		/// <summary>Dry.</summary>
		Dry = 1280, // יבש
		/// <summary>Freezing.</summary>
		Freezing = 1290, // קפיאה
		/// <summary>Frost.</summary>
		Frost = 1300, // קרה
		/// <summary>Hot.</summary>
		Hot = 1310, // חם מאד
		/// <summary>Cold.</summary>
		Cold = 1320, // קר
		/// <summary>Warm.</summary>
		Warm = 1330, // התחממות
		/// <summary>Cool.</summary>
		Cool = 1340, // התקררות
		/// <summary>Partly cloudy with a rise in temperatures.</summary>
		PartlyCloudyTempRise = 1350, // מעונן חלקית עם עליה בטמפ'
		/// <summary>Partly cloudy with a decrease in temperatures.</summary>
		PartlyCloudyTempDecrease = 1360, // מעונן חלקית עם ירידה בטמפ'
		/// <summary>Partly cloudy with a significant rise in temperatures.</summary>
		PartlyCloudySigTempRise = 1370, // מעונן חלקית עם עליה ניכרת בטמפרטורת
		/// <summary>Partly cloudy with a significant decrease in temperatures.</summary>
		PartlyCloudySigTempDecrease = 1380, // מעונן חלקית עם ירידה ניכרת בטמפרטורות
		/// <summary>Cloudy with a significant rise in temperatures.</summary>
		CloudySigTempRise = 1390, // מעונן עם עליה ניכרת בטמפ'
		/// <summary>Cloudy with a significant decrease in temperatures.</summary>
		CloudySigTempDecrease = 1400, // מעונן עם ירידה ניכרת בטמפ'
		/// <summary>Cloudy with a rise in temperatures.</summary>
		CloudyTempRise = 1410, // מעונן עם עליה בטמפ'
		/// <summary>Cloudy with a decrease in temperatures.</summary>
		CloudyTempDecrease = 1420, // מעונן עם ירידה בטמפ'
		/// <summary>Partly cloudy, local rain.</summary>
		PartlyCloudyWithRain = 1430, // מעונן חלקית עם גשם מקומי
		/// <summary>Significant rise in temperatures.</summary>
		SigTempRise = 1440, // עליה ניכרת בטמפ'
		/// <summary>Significant drop in temperatures.</summary>
		SigTempDecrease = 1450, // ירידה ניכרת בטמפ'
		/// <summary>Very hot and dry</summary>
		HotAndDry = 1460, // שרבי
	}

	public enum SeaStatus
	{
		/// <summary>No code, or invalid code.</summary>
		None,
		/// <summary>Calm.</summary>
		Calm = 10, // דומם
		/// <summary>Rippled.</summary>
		Rippled = 20, // שקט
		/// <summary>Smooth.</summary>
		Smooth = 30, // נח
		/// <summary>Smooth to slight.</summary>
		SmoothToSlight = 40, // נח עד גלי
		/// <summary>Slight.</summary>
		Slight = 50, // גלי
		/// <summary>Slight to moderate.</summary>
		SlightToModerate = 55, // גלי עד גבה גלים
		/// <summary>Moderate.</summary>
		Moderate = 60, // גבה גלים
		/// <summary>Moderate to rough.</summary>
		ModerateToRough = 70, // גבה גלים עד רוגש
		/// <summary>Rough.</summary>
		Rough = 80, // רוגש
		/// <summary>Rough to very rough.</summary>
		RoughToVeryRough = 90, // רוגש עד סוער
		/// <summary>Very rough.</summary>
		VeryRough = 110, // סוער
		/// <summary>Very rough to high.</summary>
		VeryRoughToHigh = 120, // סוער עד גועש
		/// <summary>High.</summary>
		High = 130, // גועש
		/// <summary>High to very high.</summary>
		HighToVeryHigh = 140, // גועש עד זועף
		/// <summary>Very high.</summary>
		VeryHigh = 150, // זועף
		/// <summary>Phenomenal.</summary>
		Phenomenal = 160, // זועף מאד
		/// <summary>Smooth. Becoming slight.</summary>
		SmoothBecomingSlight = 161, // נח בתחילה. יעשה גלי
		/// <summary>Smooth. Becoming slight during day time.</summary>
		SmoothBcomingSlightToday = 162, // נח. במשך היום יעשה גלי
		/// <summary>Smooth. Tomorrow would become slight to moderate.</summary>
		SmoothBecomingSlightToModerateTomorrow = 163, // נח. יעשה גלי עד גבה גלים מחר.
		/// <summary>Smooth. Becoming slight to moderate.</summary>
		SmoothBecomingSlightToModerate = 164, //  נח. יעשה גלי עד גבה גלים
		/// <summary>Smooth to slight. Becoming moderate.</summary>
		SmoothToSlightBecomingModerate = 165, // נח עד גלי. יעשה גבה גלים
		/// <summary>Smooth at the west coast, slight at the east coast.</summary>
		SmoothWestSlightEast = 166, // נח בגדה המערבית, גלי בגדה המזרחית
		/// <summary>Smooth to slight. Becoming slight to moderate.</summary>
		SmoothToSlightBecomingSlightToModerate = 167, // נח עד גלי. יעשה גלי עד גבה גלים
		/// <summary>Slight. Becoming moderate.</summary>
		SlightBecomingModerate = 168, // גלי. יעשה גבה גלים
		/// <summary>Smooth to slight. Becoming moderate to rough.</summary>
		SmoothToSlightBecomingModerateToRough = 169, // נח עד גלי. יעשה גבה גלים עד רוגש
		/// <summary>Slight over the Western coast, moderate over the Eastern coast.</summary>
		SlightWestModerateEast = 170, // גלי בגדה המערבית, גבה גלים בגדה המזרחית
		/// <summary>Slight to moderate. Becoming moderate to rough.</summary>
		SlightToModerateBecomingModerateToRough = 171, // גלי עד גבה גלים. יעשה גבה גלים עד רוגש
	}
}