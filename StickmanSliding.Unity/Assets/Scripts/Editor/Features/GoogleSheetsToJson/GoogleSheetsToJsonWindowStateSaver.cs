using StickmanSliding.Editor.Features.GoogleSheetsToJson.Data.Dynamic.State;
using static StickmanSliding.Editor.Features.GoogleSheetsToJson.Data.Static.Constants.GoogleSheetsToJsonWindowConstants;
using static UnityEditor.EditorPrefs;


namespace StickmanSliding.Editor.Features.GoogleSheetsToJson
{
    public class GoogleSheetsToJsonWindowStateSaver
    {
        public void Save(GoogleSheetsToJsonWindowState state)
        {
            SetString(SpreadsheetIdEditorPrefsKey,       state.SpreadsheetId);
            SetString(SpreadsheetPageIdEditorPrefsKey,   state.SpreadsheetPageId);
            SetString(JsonStorageFilePathEditorPrefsKey, state.JsonStorageFilePath);
            SetString(FileNameEditorPrefsKey,            state.FileName);
        }

        public void Load(GoogleSheetsToJsonWindowState state)
        {
            state.SpreadsheetId       = GetString(SpreadsheetIdEditorPrefsKey,       DefaultSpreadsheetId);
            state.SpreadsheetPageId   = GetString(SpreadsheetPageIdEditorPrefsKey,   DefaultSpreadsheetPageId);
            state.JsonStorageFilePath = GetString(JsonStorageFilePathEditorPrefsKey, DefaultJsonStorageFilePath);
            state.FileName            = GetString(FileNameEditorPrefsKey,            DefaultFileName);
        }
    }
}