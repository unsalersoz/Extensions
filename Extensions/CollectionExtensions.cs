using System.Collections.Generic;

namespace Extensions
{
	internal static class CollectionExtensions
	{
		public static void AddIfNotExist<T>(this ICollection<T> collection, T itemToInsert)
		{
			if (!collection.Contains(itemToInsert))
			{
				collection.Add(itemToInsert);
			}
		}

		public static void AddIfNotExist<TK, TV>(this IDictionary<TK, TV> collection, TK itemKey, TV itemValue)
		{
			if (!collection.ContainsKey(itemKey))
			{
				collection[itemKey] = itemValue;
			}
		}
	}
}