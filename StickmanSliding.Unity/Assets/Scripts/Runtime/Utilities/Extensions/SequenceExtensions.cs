using System.Collections.Generic;
using System.Linq;

namespace StickmanSliding.Utilities.Extensions
{
    public static class SequenceExtensions
    {
        public static int FirstIndex<T>(this IEnumerable<T> source) => default;
        public static int LastIndex<T>(this  IEnumerable<T> source) => source.Count() - 1;
    }
}