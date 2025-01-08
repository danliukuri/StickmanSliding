using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson
{
    public class GoogleSheetsToJsonWindow : EditorWindow
    {
        private const string PathSeparator     = "/";
        private const string DefaultWindowPath = "Window";
        private const string WindowTitle       = "Google Sheets To JSON";
        private const string WindowPath        = DefaultWindowPath + PathSeparator + WindowTitle;

        private const string SpreadsheetIdKey =
            nameof(GoogleSheetsToJsonWindow) + PathSeparator + nameof(SpreadsheetId);

        private const string SpreadsheetPageIdKey =
            nameof(GoogleSheetsToJsonWindow) + PathSeparator + nameof(SpreadsheetPageId);

        private const string JsonStorageFilePathKey =
            nameof(GoogleSheetsToJsonWindow) + PathSeparator + nameof(JsonStorageFilePath);


        private const string DefaultSpreadsheetId       = "1syVgzYdg5YfqwnOfl8l7iLSlTA3-75NdCklQVPcHWw0";
        private const string DefaultSpreadsheetPageId   = "0";
        private const string DefaultJsonStorageFilePath = "./Assets/Configurations/";

        [SerializeField] private VisualTreeAsset visualTreeAsset;

        public string SpreadsheetId;
        public string SpreadsheetPageId;
        public string JsonStorageFilePath;

        private void OnEnable()
        {
            SpreadsheetId       = EditorPrefs.GetString(SpreadsheetIdKey,       DefaultSpreadsheetId);
            SpreadsheetPageId   = EditorPrefs.GetString(SpreadsheetPageIdKey,   DefaultSpreadsheetPageId);
            JsonStorageFilePath = EditorPrefs.GetString(JsonStorageFilePathKey, DefaultJsonStorageFilePath);
        }

        private void OnDisable()
        {
            EditorPrefs.SetString(SpreadsheetIdKey,       SpreadsheetId);
            EditorPrefs.SetString(SpreadsheetPageIdKey,   SpreadsheetPageId);
            EditorPrefs.SetString(JsonStorageFilePathKey, JsonStorageFilePath);
        }

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            root.dataSource = this;

            VisualElement assetUI = visualTreeAsset.Instantiate();
            root.Add(assetUI);

            assetUI.Q(nameof(SpreadsheetId)).AddManipulator(new ContextualMenuManipulator(populateEvent =>
            {
                populateEvent.menu.AppendAction(actionName: "Reset", action =>
                    SpreadsheetId = DefaultSpreadsheetId);
            }));
            assetUI.Q(nameof(SpreadsheetPageId)).AddManipulator(new ContextualMenuManipulator(populateEvent =>
            {
                populateEvent.menu.AppendAction(actionName: "Reset", action =>
                    SpreadsheetPageId = DefaultSpreadsheetPageId);
            }));
            assetUI.Q(nameof(JsonStorageFilePath)).AddManipulator(new ContextualMenuManipulator(populateEvent =>
            {
                populateEvent.menu.AppendAction(actionName: "Reset", action =>
                    JsonStorageFilePath = DefaultJsonStorageFilePath);
            }));

            assetUI.Q<Button>("DownloadAndParseToJson").clicked += DownloadAndParse;
        }

        [MenuItem(WindowPath)]
        public static void ShowExample() => GetWindow<GoogleSheetsToJsonWindow>(WindowTitle);

        private void DownloadAndParse() => Debug.Log("Button clicked!");
    }
}