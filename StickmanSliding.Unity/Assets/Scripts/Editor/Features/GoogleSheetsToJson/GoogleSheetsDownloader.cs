using System.Threading.Tasks;
using StickmanSliding.Editor.Features.GoogleSheetsToJson.Data.Dynamic.State;
using UnityEngine;
using UnityEngine.Networking;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson
{
    public class GoogleSheetsDownloader
    {
        private const string UrlExportFormat = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}";
        private const string UrlEditFormat   = "https://docs.google.com/spreadsheets/d/{0}/edit?gid={1}";

        public async Task<string> DownloadCsv(GoogleSheetsToJsonWindowState state)
        {
            string urlToEdit = string.Format(UrlEditFormat, state.SpreadsheetId, state.SpreadsheetPageId);
            Debug.Log($"Started downloading spreadsheet from <a href=\"{urlToEdit}\">{urlToEdit}</a>");

            string url = string.Format(UrlExportFormat, state.SpreadsheetId, state.SpreadsheetPageId);

            using UnityWebRequest request = UnityWebRequest.Get(url);

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Finished downloading spreadsheet with success");
                return request.downloadHandler.text;
            }

            Debug.LogError("Finished downloading spreadsheet with error: " + request.error);
            return null;
        }
    }
}