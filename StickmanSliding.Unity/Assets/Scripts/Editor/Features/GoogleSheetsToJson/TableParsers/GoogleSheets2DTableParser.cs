using System;
using System.Collections.Generic;
using StickmanSliding.Editor.Features.GoogleSheetsToJson.StringParsers;
using StickmanSliding.Editor.Features.GoogleSheetsToJson.Utilities;
using UnityEngine;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson.TableParsers
{
    public class GoogleSheets2DTableParser : IGoogleSheetsTableParser
    {
        private readonly TypeInstancesProvider _instancesProvider = new();

        public string Type => "2D";

        public Dictionary<string, object> Parse(List<List<string>>    table,
                                                (int Row, int Column) startIndex,
                                                int                   height,
                                                int                   width)
        {
            const int tableDataTypeRowOffset    = 2;
            const int tableDataTypeColumnOffset = 0;

            string tableDataType =
                table[startIndex.Row + tableDataTypeRowOffset][startIndex.Column + tableDataTypeColumnOffset];

            List<IStringParser> stringParsers = _instancesProvider.GetInstances<IStringParser>();

            IStringParser dataTypeParser = stringParsers.Find(parser =>
                string.Equals(parser.Type, tableDataType, StringComparison.OrdinalIgnoreCase));

            Dictionary<string, object> parsedData = null;

            if (dataTypeParser != null)
            {
                List<object> tableData = ParseTableData(table, startIndex, height, width, dataTypeParser);

                if (tableData != null)
                {
                    const int tableNameTypeRowOffset    = 0;
                    const int tableNameTypeColumnOffset = 1;

                    string tableName =
                        table[startIndex.Row + tableNameTypeRowOffset][startIndex.Column + tableNameTypeColumnOffset];

                    parsedData = new Dictionary<string, object> { { tableName, tableData } };
                }
            }

            return parsedData;
        }

        private List<object> ParseTableData(List<List<string>>    table,
                                            (int Row, int Column) startIndex,
                                            int                   height,
                                            int                   width,
                                            IStringParser         dataTypeParser)
        {
            List<object> arrayData = new();

            const int tableContentRowOffset    = 1;
            const int tableContentColumnOffset = 1;

            int contentRowOffset    = startIndex.Row    + tableContentRowOffset;
            int contentColumnOffset = startIndex.Column + tableContentColumnOffset;

            for (int i = contentRowOffset; i < contentRowOffset + height; i++)
                for (int j = contentColumnOffset; j < contentColumnOffset + width; j++)
                    try
                    {
                        arrayData.Add(dataTypeParser.Parse(table[i][j]));
                    }
                    catch (FormatException exception)
                    {
                        Debug.LogError($"Error parsing cell {j.ToExcelColumnName()}{i + 1} \"{table[i][j]}\" " +
                                       $"to {dataTypeParser.Type}: {exception.Message}");
                        return null;
                    }

            return arrayData;
        }
    }
}