using static StickmanSliding.Editor.Data.Static.Constants.GoogleSheetsToJsonWindowVisualElementsNameConstants;

namespace StickmanSliding.Editor.Data.Static.Constants
{
    public class GoogleSheetsToJsonWindowConstants
    {
        public const string PathSeparator     = "/";
        public const string DefaultWindowPath = "Window";
        public const string WindowTitle       = "Google Sheets To JSON";
        public const string WindowPath        = DefaultWindowPath + PathSeparator + WindowTitle;

        public const string DefaultSpreadsheetId       = "1syVgzYdg5YfqwnOfl8l7iLSlTA3-75NdCklQVPcHWw0";
        public const string DefaultSpreadsheetPageId   = "0";
        public const string DefaultJsonStorageFilePath = "./Assets/Configurations/";

        public const string SpreadsheetIdEditorPrefsKey       = WindowPath + PathSeparator + SpreadsheetId;
        public const string SpreadsheetPageIdEditorPrefsKey   = WindowPath + PathSeparator + SpreadsheetPageId;
        public const string JsonStorageFilePathEditorPrefsKey = WindowPath + PathSeparator + JsonStorageFilePath;
    }
}