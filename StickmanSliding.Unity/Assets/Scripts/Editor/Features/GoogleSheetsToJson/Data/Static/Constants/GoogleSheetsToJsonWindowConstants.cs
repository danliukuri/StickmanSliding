using static StickmanSliding.Editor.Features.GoogleSheetsToJson.Data.Static.Constants.GoogleSheetsToJsonWindowVisualElementsNameConstants;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson.Data.Static.Constants
{
    public class GoogleSheetsToJsonWindowConstants
    {
        public const string PathSeparator     = "/";
        public const string DefaultWindowPath = "Window";
        public const string WindowTitle       = "Google Sheets To JSON";
        public const string WindowPath        = DefaultWindowPath + PathSeparator + WindowTitle;

        public const string DefaultSpreadsheetId       = "1syVgzYdg5YfqwnOfl8l7iLSlTA3-75NdCklQVPcHWw0";
        public const string DefaultSpreadsheetPageId   = "0";
        public const string DefaultJsonStorageFilePath = "Assets/Configurations";
        public const string DefaultFileName            = "Data";

        public const string SpreadsheetIdEditorPrefsKey       = WindowPath + PathSeparator + SpreadsheetId;
        public const string SpreadsheetPageIdEditorPrefsKey   = WindowPath + PathSeparator + SpreadsheetPageId;
        public const string JsonStorageFilePathEditorPrefsKey = WindowPath + PathSeparator + JsonStorageFilePath;
        public const string FileNameEditorPrefsKey            = WindowPath + PathSeparator + FileName;
    }
}