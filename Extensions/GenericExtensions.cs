using System;
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

		public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => (collection == null) || !collection.Any();

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> inputCollection, Func<T, T> function)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (T t in inputCollection)
            {
                yield return function.EndInvoke(function.BeginInvoke(t, null, null));
            }
        }

        public static bool TryAddItem<T>(this IEnumerable<T> inputCollection, T itemToAdd, out ICollection<T> outputCollection) where T : IEqualityComparer<T>
        {
            bool result = false;
            outputCollection = null;

            if (inputCollection.All(item => !item.Equals(itemToAdd)))
            {
                inputCollection = inputCollection.Concat(new[] { itemToAdd });
                outputCollection = inputCollection.ToArray();
                result = true;
            }

            return result;
        }
    }
}