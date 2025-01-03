using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StickmanSliding.Utilities.Extensions
{
    public static class VectorLinqExtensions
    {
        public static Vector3 Average<TSource>(this IEnumerable<TSource> source, Func<TSource, Vector3> selector)
        {
            (Vector3 vectorSum, int count) = source.Aggregate((VectorSum: Vector3.zero, Count: 0),
                (current, item) => (current.VectorSum + selector.Invoke(item), current.Count + 1));

            if (count == 0)
                throw new InvalidOperationException("Sequence contains no elements");

            return vectorSum / count;
        }
    }
}