using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
	internal static class GenericExtensions
	{
		internal static bool IsIn<T>(this T objectToSearch, params T[] collection) => collection.Contains(objectToSearch);

		public static T ShallowClone<T>(this T value) where T : new()
		{
			T result = new T();
			
			value.GetType().GetFields().AsParallel().ForAll(fieldInfo => result.GetType().GetField(fieldInfo.Name).SetValue(result, value.GetType().GetField(fieldInfo.Name).GetValue(value)));
			
			return result;
		}

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || !collection.Any();
	}
}