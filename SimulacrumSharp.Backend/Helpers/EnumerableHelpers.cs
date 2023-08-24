namespace SimulacrumSharp.Backend.Helpers
{
    public static class EnumerableHelpers
    {
        public static int Count = 0;

        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                list.Add(item);
            }
        }

        public static IEnumerable<T> Mode<T>(this IEnumerable<T> collection)
        {
            var dictionary = collection.ToLookup(x => x);
            if (dictionary.Count.Equals(0))
            {
                return Enumerable.Empty<T>();
            }
            var maxCount = dictionary.Max(x => x.Count());
            return dictionary
                .Where(x => x.Count().Equals(maxCount))
                .Select(x => x.Key);
        }
    }
}
