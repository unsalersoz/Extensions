using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

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

        /// <summary>
		/// An unsafe, slow but more byte-to-byte copy of an object into a byte array.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static byte[] ToByteArrayMarshal(this object input)
        {
            byte[] result;

            try
            {
                int size = Marshal.SizeOf(input);
                result = new byte[size];
                IntPtr ptr = Marshal.AllocHGlobal(size);

                Marshal.StructureToPtr(input, ptr, false);
                Marshal.Copy(ptr, result, 0, size);
                Marshal.FreeHGlobal(ptr);
            }
            catch (Exception exception)
            {
                result = null;
                throw new InvalidCastException("An error occured while converting object to byte array with marshalling", exception);
            }

            return result;
        }

        public static byte[] ToByteArraySafe(this object input)
        {
            byte[] result;

            if (input != null)
            {
                using (MemoryStream outputstream = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(outputstream, input);
                    result = outputstream.ToArray();
                }
            }
            else
            {
                result = null;
            }

            return result;
        }
    }
}