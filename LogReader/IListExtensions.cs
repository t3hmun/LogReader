namespace LogReader
{
    using System.Collections.Generic;

    public static class IListExtensions
    {
        /// <summary>
        ///     Enumerates an interval in an IList. A faster alternative to Skip(..).Take(..) when index is available.
        /// </summary>
        /// <param name="list">The IList to enumerate entries from.</param>
        /// <param name="startInclusive">The starting index.</param>
        /// <param name="endInclusive">The final index.</param>
        /// <returns>The items from the start index to the end index inclusive.</returns>
        public static IEnumerable<T> SubList<T>(this IList<T> list, int startInclusive, int endInclusive)
        {
            for (var i = startInclusive; i <= endInclusive; i++) yield return list[i];
        }
    }
}