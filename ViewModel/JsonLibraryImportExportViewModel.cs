using Playnite.SDK;
using Newtonsoft.Json;
using Playnite.SDK.Models;
using System.Collections.Generic;
using System.IO;

namespace JsonLibraryImportExport.ViewModel
{
    class JsonLibraryImportExportViewModel
    {
        private readonly IPlayniteAPI playniteApi;

        public JsonLibraryImportExportSettingsViewModel Settings { get; }

        public JsonLibraryImportExportViewModel(IPlayniteAPI playniteApi, JsonLibraryImportExportSettingsViewModel settings)
        {
            this.playniteApi = playniteApi;
            Settings = settings;
        }

        public RelayCommand ExportAllGamesCommand
        {
            get => new RelayCommand(() =>
            {
                ExportGamesToJson();
            });
        }

        public void ExportGamesToJson()
        {
            var folderPath = playniteApi.Dialogs.SelectFolder();
            if (string.IsNullOrEmpty(folderPath))
            {
                return;
            }
            IEnumerable<Game> games = playniteApi.Database.Games;
            var jsonString = JsonConvert.SerializeObject(games, Formatting.Indented);
            string fullPathGames = Path.Combine(folderPath, "games.json");
            File.WriteAllText(fullPathGames, jsonString);
        }

        public RelayCommand ImportAllGamesCommand
        {
            get => new RelayCommand(() =>
            {
                ImportGamesFromJson();
            });
        }

        public void ImportGamesFromJson()
        {
            var filePath = playniteApi.Dialogs.SelectFile("json|*.json");
            var jsonString = File.ReadAllText(filePath);

            IEnumerable<Game> games = JsonConvert.DeserializeObject<IEnumerable<Game>>(jsonString);

            foreach (var game in games)
            {
                if (!playniteApi.Database.Games.Contains(game))
                {
                    playniteApi.Database.Games.Add(game);
                }
                else
                {
                    playniteApi.Database.Games.Update(game);
                }
            }
        }
    }
}
