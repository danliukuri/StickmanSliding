using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using StickmanSliding.Editor.Features.GoogleSheetsToJson.TableParsers;
using UnityEngine;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson
{
    public class GoogleSheetsParser
    {
        private readonly TypeInstancesProvider _instancesProvider = new();

        public Dictionary<string, object> ParseCsv(string csvData)
        {
            Debug.Log("Started parsing CSV data");

            const string rowsSplitPattern        = "\r\n|\n\r|\n|\r";
            const string rowsDelimitersPattern   = "\"([^\"]*)\"";
            const string columnSplitPattern      = ",";
            const string columnDelimitersPattern = "\"([^\"]*)\"";

            List<string> rows = SplitIgnoringDelimiters(csvData, rowsSplitPattern, rowsDelimitersPattern);
            List<List<string>> spreadsheetTable =
                rows.Select(row => SplitIgnoringDelimiters(row, columnSplitPattern, columnDelimitersPattern)).ToList();

            Dictionary<string, object> parsedData = ParseSpreadsheetTable(spreadsheetTable);

            Debug.Log("Finished parsing CSV data");
            return parsedData;
        }

        private List<string> SplitIgnoringDelimiters(string input, string splitPattern, string delimitersPattern)
        {
            IEnumerable<(int Start, int End)> ignoreRanges =
                Regex.Matches(input, delimitersPattern).Select(match => (match.Index, match.Index + match.Length));

            (List<string> Lines, int LastSplitIndex) result = Regex.Matches(input, splitPattern)
                .Where(match => !ignoreRanges.Any(range => match.Index >= range.Start && match.Index < range.End))
                .Aggregate((Lines: new List<string>(), LastSplitIndex: 0), (data, match) =>
                {
                    data.Lines.Add(input.Substring(data.LastSplitIndex, match.Index - data.LastSplitIndex));
                    data.LastSplitIndex = match.Index + match.Length;
                    return data;
                });

            result.Lines.Add(input[result.LastSplitIndex..]);
            return result.Lines;
        }

        private Dictionary<string, object> ParseSpreadsheetTable(List<List<string>> table)
        {
            var parsedData = new Dictionary<string, object>();

            for (var i = 0; i < table.Count; i++)
                for (var j = 0; j < table[i].Count; j++)
                {
                    object tableData = ParseTable(table, (i, j));
                    if (tableData != null)
                    {
                        const int tableNameTypeRowOffset    = 0;
                        const int tableNameTypeColumnOffset = 1;

                        string tableName = table[i + tableNameTypeRowOffset][j + tableNameTypeColumnOffset];

                        parsedData.Add(tableName, tableData);
                    }
                }

            return parsedData;
        }

        private object ParseTable(List<List<string>> table, (int Row, int Column) startIndex)
        {
            const string tableStartMarker = "start";
            const string tableEndMarker   = "end";

            object tableData = null;

            if (table[startIndex.Row][startIndex.Column] == tableStartMarker)
            {
                int endMarkerRowIndex =
                    table.FindIndex(startIndex.Row + 1, row => row[startIndex.Column] == tableEndMarker);
                int height = endMarkerRowIndex - startIndex.Row - 1;

                int endMarkerColumnIndex =
                    table[startIndex.Row].FindIndex(startIndex.Column + 1, cell => cell == tableEndMarker);
                int width = endMarkerColumnIndex - startIndex.Column - 1;

                if (height > 0 && width > 0)
                {
                    const int tableTypeRowOffset    = 1;
                    const int tableTypeColumnOffset = 0;

                    string tableType =
                        table[startIndex.Row + tableTypeRowOffset][startIndex.Column + tableTypeColumnOffset];

                    List<IGoogleSheetsTableParser> tableParsers =
                        _instancesProvider.GetInstances<IGoogleSheetsTableParser>();

                    IGoogleSheetsTableParser tableParser = tableParsers.Find(parser =>
                        string.Equals(parser.Type, tableType, StringComparison.OrdinalIgnoreCase));

                    tableData = tableParser?.Parse(table, startIndex, height, width);
                }
            }

            return tableData;
        }
    }
}