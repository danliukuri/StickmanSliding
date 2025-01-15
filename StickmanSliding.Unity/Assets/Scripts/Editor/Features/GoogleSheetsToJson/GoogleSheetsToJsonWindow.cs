using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StickmanSliding.Editor.Data.Dynamic.State;
using Unity.Plastic.Newtonsoft.Json;
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
        private readonly GoogleSheetsParser                 _parser     = new();

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
            const string fileName      = "DATA";
            const string csvExtension  = ".csv";
            const string jsonExtension = ".json";
            try
            {
                string rawCsvData = await _downloader.DownloadCsv(_state);
                if (!string.IsNullOrEmpty(rawCsvData))
                {
                    Debug.Log("Started saving CSV data");
                    string csvStorageFilePath = Path.Join(_state.JsonStorageFilePath, fileName + csvExtension);
                    await File.WriteAllTextAsync(csvStorageFilePath, rawCsvData);
                    Debug.Log("Finished saving CSV data to "                                        +
                              $"<a href=\"{csvStorageFilePath}\">{fileName + csvExtension}</a> at " +
                              $"<a href=\"{_state.JsonStorageFilePath}\">{_state.JsonStorageFilePath}</a>");

                    List<object> parsedData = _parser.ParseCsv(rawCsvData);
                    if (parsedData != null && parsedData.Any())
                    {
                        Debug.Log("Started converting parsed data to JSON");
                        string jsonData = JsonConvert.SerializeObject(parsedData);
                        Debug.Log("Finished converting parsed data to JSON");

                        Debug.Log("Started saving JSON data");
                        string jsonStorageFilePath = Path.Join(_state.JsonStorageFilePath, fileName + jsonExtension);
                        await File.WriteAllTextAsync(jsonStorageFilePath, jsonData);
                        Debug.Log("Finished saving JSON data to "                                         +
                                  $"<a href=\"{jsonStorageFilePath}\">{fileName + jsonExtension}</a> at " +
                                  $"<a href=\"{_state.JsonStorageFilePath}\">{_state.JsonStorageFilePath}</a>");
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(exception);
            }
        }
    }
}