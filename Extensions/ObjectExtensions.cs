using System;
using System.ComponentModel;

namespace Extensions
{
	public static class ObjectExtensions
	{
		public static T ToType<T>(this object input) where T : IConvertible => (T) Convert.ChangeType(input, typeof (T));

		public static T? ToNullable<T>(this object input) where T : struct
		{
			T? result = new T?();

			TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof (T));

			if (!string.IsNullOrEmpty(input?.ToString()))
			{
				object convertFrom;
				if (typeof (T) == typeof (bool))
				{
					string parsedInput;
					switch (input.ToString())
					{
						case "0":
							parsedInput = "false";
							break;
						case "1":
							parsedInput = "true";
							break;
						default:
							parsedInput = input.ToString();
							break;
					}

					convertFrom = typeConverter.ConvertFrom(parsedInput);
				}
				else
				{
					convertFrom = typeConverter.ConvertFrom(input.ToString());
				}

				if (convertFrom != null)
				{
					result = (T) convertFrom;
				}
			}

			return result;
		}

		public static DateTimeOffset? TryToNullableDateTimeOffset(this object input)
		{
			DateTimeOffset output;
			return DateTimeOffset.TryParse(input.ToString(), out output) ? output : (DateTimeOffset?) null;
		}
	}
}