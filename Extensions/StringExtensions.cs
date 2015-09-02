﻿using System;
using System.Globalization;

namespace Extensions
{
	public static class StringExtensions
	{
		private const string SIMPLE_DATETIME_FORMAT = "yyyy-MM-dd";
		private const string DETAILED_DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

		public static DateTimeOffset ToTurkishDateTimeOffset(this string input) => DateTimeOffset.ParseExact(input, SIMPLE_DATETIME_FORMAT, CultureInfo.GetCultureInfo("tr-TR"), DateTimeStyles.None);

		public static DateTimeOffset ToDetailedTurkishDateTimeOffset(this string input) => DateTimeOffset.ParseExact(input, DETAILED_DATETIME_FORMAT, CultureInfo.GetCultureInfo("tr-TR"), DateTimeStyles.None);

		public static DateTime ToTurkishDateTime(this string input) => DateTime.ParseExact(input, SIMPLE_DATETIME_FORMAT, CultureInfo.GetCultureInfo("tr-TR"), DateTimeStyles.None);

		public static DateTime ToDetailedTurkishDateTime(this string input) => DateTime.ParseExact(input, DETAILED_DATETIME_FORMAT, CultureInfo.GetCultureInfo("tr-TR"), DateTimeStyles.None);

		public static string NullString(this string input) => string.IsNullOrEmpty(input) ? null : input;

		public static string NullZeroString(this string input) => string.IsNullOrEmpty(input) || input.Equals("0")
			? null
			: input;
	}
}