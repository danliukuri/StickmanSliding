using System;
using System.IO;
using StickmanSliding.Editor.Data.Dynamic.State;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static StickmanSliding.Editor.Data.Static.Constants.GoogleSheetsToJsonWindowConstants;
using static StickmanSliding.Editor.Data.Static.Constants.GoogleSheetsToJsonWindowVisualElementsNameConstants;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson
{
    public class GoogleSheetsToJsonWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset visualTreeAsset;

        private readonly GoogleSheetsToJsonWindowState      _state      = new();
        private readonly GoogleSheetsToJsonWindowStateSaver _stateSaver = new();
        private readonly GoogleSheetsDownloader             _downloader = new();

        private void OnEnable() => _stateSaver.Load(_state);

        private void OnDisable() => _stateSaver.Save(_state);

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            root.dataSource = _state;

            VisualElement assetUI = visualTreeAsset.Instantiate();
            root.Add(assetUI);

            assetUI.Q(nameof(_state.SpreadsheetId)).AddManipulator(new ContextualMenuManipulator(populateEvent =>
            {
                populateEvent.menu.AppendAction(Reset, action =>
                    _state.SpreadsheetId = DefaultSpreadsheetId);
            }));
            assetUI.Q(nameof(_state.SpreadsheetPageId)).AddManipulator(new ContextualMenuManipulator(populateEvent =>
            {
                populateEvent.menu.AppendAction(Reset, action =>
                    _state.SpreadsheetPageId = DefaultSpreadsheetPageId);
            }));
            assetUI.Q(nameof(_state.JsonStorageFilePath)).AddManipulator(new ContextualMenuManipulator(populateEvent =>
            {
                populateEvent.menu.AppendAction(Reset, action =>
                    _state.JsonStorageFilePath = DefaultJsonStorageFilePath);
            }));

            assetUI.Q<Button>(DownloadAndParseToJson).clicked += DownloadAndParse;
        }

        [MenuItem(WindowPath)]
        public static void ShowExample() => GetWindow<GoogleSheetsToJsonWindow>(WindowTitle);

        private async void DownloadAndParse()
        {
            try
            {
                string rawCsvData = await _downloader.DownloadCsv(_state);
                if (!string.IsNullOrEmpty(rawCsvData))
                    await File.WriteAllTextAsync(_state.JsonStorageFilePath + "DATA.csv", rawCsvData);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception);
            }
        }
    }
}