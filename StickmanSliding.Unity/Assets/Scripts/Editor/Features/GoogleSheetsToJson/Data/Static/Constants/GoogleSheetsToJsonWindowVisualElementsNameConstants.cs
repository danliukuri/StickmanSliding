﻿using StickmanSliding.Editor.Features.GoogleSheetsToJson.Data.Dynamic.State;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson.Data.Static.Constants
{
    public class GoogleSheetsToJsonWindowVisualElementsNameConstants
    {
        public const string SpreadsheetId          = nameof(GoogleSheetsToJsonWindowState.SpreadsheetId);
        public const string SpreadsheetPageId      = nameof(GoogleSheetsToJsonWindowState.SpreadsheetPageId);
        public const string JsonStorageFilePath    = nameof(GoogleSheetsToJsonWindowState.JsonStorageFilePath);
        public const string FileName               = nameof(GoogleSheetsToJsonWindowState.FileName);
        public const string DownloadAndParseToJson = nameof(DownloadAndParseToJson);

        public const string Reset = nameof(Reset);
    }
}