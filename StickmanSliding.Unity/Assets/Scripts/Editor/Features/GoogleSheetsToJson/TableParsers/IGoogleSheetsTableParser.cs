using System.Collections.Generic;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson.TableParsers
{
    public interface IGoogleSheetsTableParser
    {
        string Type { get; }

        Dictionary<string, object> Parse(List<List<string>>    table,
                                         (int Row, int Column) startIndex,
                                         int                   height,
                                         int                   width);
    }
}