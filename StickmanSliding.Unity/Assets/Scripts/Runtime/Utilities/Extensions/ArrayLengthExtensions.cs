namespace StickmanSliding.Utilities.Extensions
{
    public static class ArrayLengthExtensions
    {
        private const int FirstArrayDimensionIndex  = 0;
        private const int SecondArrayDimensionIndex = 1;

        public static int RowLength<T>(this    T[,] source) => source.GetLength(FirstArrayDimensionIndex);
        public static int ColumnLength<T>(this T[,] source) => source.GetLength(SecondArrayDimensionIndex);

        public static int RowFirstIndex<T>(this T[,] source) => source.GetLowerBound(FirstArrayDimensionIndex);
        public static int RowLastIndex<T>(this  T[,] source) => source.GetUpperBound(FirstArrayDimensionIndex);

        public static int ColumnFirstIndex<T>(this T[,] source) => source.GetLowerBound(SecondArrayDimensionIndex);
        public static int ColumnLastIndex<T>(this  T[,] source) => source.GetUpperBound(SecondArrayDimensionIndex);
    }
}