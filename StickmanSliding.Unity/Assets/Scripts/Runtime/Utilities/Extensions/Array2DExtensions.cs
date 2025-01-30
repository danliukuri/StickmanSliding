using System.Collections.Generic;
using System.Linq;

namespace StickmanSliding.Utilities.Extensions
{
    public static class Array2DExtensions
    {
        private const int FirstArrayDimensionIndex  = 0;
        private const int SecondArrayDimensionIndex = 1;

        public static int RowLength<T>(this    T[,] source) => source.GetLength(FirstArrayDimensionIndex);
        public static int ColumnLength<T>(this T[,] source) => source.GetLength(SecondArrayDimensionIndex);

        public static int RowFirstIndex<T>(this T[,] source) => source.GetLowerBound(FirstArrayDimensionIndex);
        public static int RowLastIndex<T>(this  T[,] source) => source.GetUpperBound(FirstArrayDimensionIndex);

        public static int ColumnFirstIndex<T>(this T[,] source) => source.GetLowerBound(SecondArrayDimensionIndex);
        public static int ColumnLastIndex<T>(this  T[,] source) => source.GetUpperBound(SecondArrayDimensionIndex);

        public static IEnumerable<int> RowIndexes<T>(this T[,] source) =>
            Enumerable.Range(source.RowFirstIndex(), source.RowLength());

        public static IEnumerable<int> ColumnIndexes<T>(this T[,] source) =>
            Enumerable.Range(source.ColumnFirstIndex(), source.ColumnLength());
        
    }
}