using System.Collections.Generic;

namespace HeapAndStackMemoryOptimization
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TResult> CastFromObject<TResult>(this IEnumerable<object> objects)
        {
            foreach (var obj in objects)
            {
                yield return (TResult) obj;
            }
        }

        public static IEnumerable<object> CastFromValueTypeToObject<TSource>(this IEnumerable<TSource> items) where TSource : struct
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }
    }
}