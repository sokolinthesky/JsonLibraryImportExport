using Playnite.SDK;
using Newtonsoft.Json;
using Playnite.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

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

        public RelayCommand ExportCommand
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
            export(playniteApi.Database.Games, folderPath, "games.json", Settings.Settings.Games);
            export(playniteApi.Database.Genres, folderPath, "genres.json", Settings.Settings.Genres);
            export(playniteApi.Database.Categories, folderPath, "categories.json", Settings.Settings.Categories);
            export(playniteApi.Database.CompletionStatuses, folderPath, "completionstatuses.json", Settings.Settings.CompletionStatuses);
            export(playniteApi.Database.Features, folderPath, "features.json", Settings.Settings.Features);
            export(playniteApi.Database.Platforms, folderPath, "platforms.json", Settings.Settings.Platforms);
            export(playniteApi.Database.Regions, folderPath, "regions.json", Settings.Settings.Regions);
            export(playniteApi.Database.Series, folderPath, "series.json", Settings.Settings.Series);
            export(playniteApi.Database.Sources, folderPath, "sources.json", Settings.Settings.Sources);
            export(playniteApi.Database.Tags, folderPath, "tags.json", Settings.Settings.Tags);
        }

        private void export(IEnumerable items, string folderPath, string filename, bool selected)
        {
            if (!selected)
            {
                return;
            }
            var jsonString = JsonConvert.SerializeObject(items, Formatting.Indented);
            File.WriteAllText(Path.Combine(folderPath, filename), jsonString);
        }

        public RelayCommand ImportCommand
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
