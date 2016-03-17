using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
	internal static class CollectionExtensions
	{
        public static bool AddIfNotExist<T>(this ICollection<T> inputCollection, T itemToAdd)
        {
            bool addSuccessFull = false;

            if (inputCollection.All(item => !item.Equals(itemToAdd)))
            {
                inputCollection.Add(itemToAdd);
                addSuccessFull = true;
            }

            return addSuccessFull;
        }

        public static bool AddIfNotExist<TK, TV>(this IDictionary<TK, TV> collection, TK itemKey, TV itemValue)
        {
            bool addSuccessfull = false;

			if (!collection.ContainsKey(itemKey))
			{
				collection[itemKey] = itemValue;
			    addSuccessfull = true;
			}

            return addSuccessfull;
        }
	}
}