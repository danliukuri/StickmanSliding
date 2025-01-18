using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using StickmanSliding.Editor.Features.GoogleSheetsToJson.Data.Dynamic.State;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static StickmanSliding.Editor.Features.GoogleSheetsToJson.Data.Static.Constants.GoogleSheetsToJsonWindowConstants;
using static StickmanSliding.Editor.Features.GoogleSheetsToJson.Data.Static.Constants.GoogleSheetsToJsonWindowVisualElementsNameConstants;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson
{
    // TODO: Move "Google Sheets To JSON" feature to separate custom package
    // TODO: Replace Debug.Log on custom logger
    // TODO: Replace constants on configurable values
    // TODO: Replace TableType and Type on enums
    // TODO: Make dependencies resolving more flexible
    // TODO: Fix "Unsaved Unity state" Rider warning on commit
    // TODO: Add code generation for json data models
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

            assetUI.Q(SpreadsheetId).AddManipulator(new ContextualMenuManipulator(populateEvent =>
            {
                populateEvent.menu.AppendAction(Reset, action =>
                    _state.SpreadsheetId = DefaultSpreadsheetId);
            }));
            assetUI.Q(SpreadsheetPageId).AddManipulator(new ContextualMenuManipulator(populateEvent =>
            {
                populateEvent.menu.AppendAction(Reset, action =>
                    _state.SpreadsheetPageId = DefaultSpreadsheetPageId);
            }));
            assetUI.Q(JsonStorageFilePath).AddManipulator(new ContextualMenuManipulator(populateEvent =>
            {
                populateEvent.menu.AppendAction(Reset, action =>
                    _state.JsonStorageFilePath = DefaultJsonStorageFilePath);
            }));
            assetUI.Q(FileName).AddManipulator(new ContextualMenuManipulator(populateEvent =>
            {
                populateEvent.menu.AppendAction(Reset, action =>
                    _state.FileName = DefaultFileName);
            }));

            assetUI.Q<Button>(DownloadAndParseToJson).clicked += DownloadAndParse;
        }

        [MenuItem(WindowPath)]
        public static void ShowExample() => GetWindow<GoogleSheetsToJsonWindow>(WindowTitle);

        private async void DownloadAndParse()
        {
            const string jsonExtension = ".json";
            try
            {
                string rawCsvData = await _downloader.DownloadCsv(_state);
                if (!string.IsNullOrEmpty(rawCsvData))
                {
                    Dictionary<string, object> parsedData = _parser.ParseCsv(rawCsvData);
                    if (parsedData != null && parsedData.Any())
                    {
                        Debug.Log("Started converting parsed data to JSON");
                        string jsonData = JsonConvert.SerializeObject(parsedData);
                        Debug.Log("Finished converting parsed data to JSON");

                        Debug.Log("Started saving JSON data");
                        string jsonStorageFilePath =
                            Path.Join(_state.JsonStorageFilePath, _state.FileName + jsonExtension);
                        await File.WriteAllTextAsync(jsonStorageFilePath, jsonData);
                        Debug.Log("Finished saving JSON data to "                                                +
                                  $"<a href=\"{jsonStorageFilePath}\">{_state.FileName + jsonExtension}</a> at " +
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