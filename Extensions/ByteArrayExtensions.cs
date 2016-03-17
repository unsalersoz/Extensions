using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace InfraStructureLayer.Extensions
{
	public static class ByteArrayExtensions
	{
		/// <summary>
		/// An unsafe, slow but more byte-to-byte copy of a byte array into object.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static object ToObjectMarshal(this byte[] input)
		{
			object result;

			int size = input.Length;
			IntPtr ptr = Marshal.AllocHGlobal(size);
			Marshal.Copy(input, 0, ptr, size);
			result = Marshal.PtrToStructure(ptr, typeof(object));
			Marshal.FreeHGlobal(ptr);

			return result;
		}

		public static object ToObjectSafe(this byte[] input)
		{
			object result;

			if (input != null)
			{
				using (MemoryStream inputStream = new MemoryStream(input))
				{
					BinaryFormatter formatter = new BinaryFormatter();
					result = formatter.Deserialize(inputStream);
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
