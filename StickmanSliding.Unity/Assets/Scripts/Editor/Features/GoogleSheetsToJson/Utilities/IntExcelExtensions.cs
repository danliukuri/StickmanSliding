using System;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson.Utilities
{
    public static class IntExcelExtensions
    {
        /// <param name="columnNumber">Zero-based excel column number.</param>
        public static string ToExcelColumnName(this int columnNumber)
        {
            const int firstLetter       = 'A';
            const int lastLetter        = 'Z';
            const int lettersInAlphabet = lastLetter - firstLetter + 1;

            var columnName = string.Empty;
            do
            {
                columnName = Convert.ToChar(firstLetter + columnNumber % lettersInAlphabet) + columnName;

                columnNumber = columnNumber / lettersInAlphabet - 1;
            }
            while (columnNumber >= 0);

            return columnName;
        }
    }
}