using System;
using System.Collections.Generic;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace Shelter.Webapp.Services
{
    public static class GoogleService
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private static SheetsService _service;

        public static void GetConnection()
        {
            GoogleCredential credential;

            using (var stream = new FileStream(Constants.ResourceName, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            _service = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = Constants.ApplicationName,
            });
        }

        public static IList<IList<object>> ReadEntries(string sheetName)
        {
            if (sheetName == null)
                throw new ArgumentNullException(nameof(sheetName));

            var range = $"{Constants.SheetCustomers}!A:F";
            var request = _service.Spreadsheets.Values.Get(Constants.SpreadsheetId, range);

            var response = request.Execute();
            var values = response.Values;

            return values;
        }
    }
}
