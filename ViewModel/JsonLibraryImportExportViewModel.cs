using Playnite.SDK;
using Newtonsoft.Json;
using Playnite.SDK.Models;
using System;
using System.Collections.Generic;
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
                ExportToJson();
            });
        }

        public void ExportToJson()
        {
            var folderPath = playniteApi.Dialogs.SelectFolder();
            if (string.IsNullOrEmpty(folderPath))
            {
                return;
            }
            Export(playniteApi.Database.Games, folderPath, "games.json", Settings.Settings.Games);
            Export(playniteApi.Database.Genres, folderPath, "genres.json", Settings.Settings.Genres);
            Export(playniteApi.Database.Categories, folderPath, "categories.json", Settings.Settings.Categories);
            Export(playniteApi.Database.CompletionStatuses, folderPath, "completionstatuses.json", Settings.Settings.CompletionStatuses);
            Export(playniteApi.Database.Features, folderPath, "features.json", Settings.Settings.Features);
            Export(playniteApi.Database.Platforms, folderPath, "platforms.json", Settings.Settings.Platforms);
            Export(playniteApi.Database.Regions, folderPath, "regions.json", Settings.Settings.Regions);
            Export(playniteApi.Database.Series, folderPath, "series.json", Settings.Settings.Series);
            Export(playniteApi.Database.Sources, folderPath, "sources.json", Settings.Settings.Sources);
            Export(playniteApi.Database.Tags, folderPath, "tags.json", Settings.Settings.Tags);
            playniteApi.Dialogs.ShowMessage("Selected databases sucessfully expoted to: " + folderPath);
        }

        private void Export(IEnumerable items, string folderPath, string filename, bool selected)
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
                try
                {
                    Import(Settings.Settings.GamesPath, playniteApi.Database.Games);
                    Import(Settings.Settings.GenresPath, playniteApi.Database.Genres);
                    Import(Settings.Settings.CategoriesPath, playniteApi.Database.Categories);
                    Import(Settings.Settings.FeaturesPath, playniteApi.Database.Features);
                    Import(Settings.Settings.PlatformPath, playniteApi.Database.Platforms);
                    Import(Settings.Settings.RegionsPath, playniteApi.Database.Regions);
                    Import(Settings.Settings.SeriesPath, playniteApi.Database.Series);
                    Import(Settings.Settings.SourcesPath, playniteApi.Database.Sources);
                    Import(Settings.Settings.TagsPath, playniteApi.Database.Tags);
                    Import(Settings.Settings.CompletionStatusesPath, playniteApi.Database.CompletionStatuses);
                    playniteApi.Dialogs.ShowMessage("Selected databases sucessfully imported");
                }
                catch (Exception e)
                {
                    playniteApi.Dialogs.ShowErrorMessage("Unaable to import selected databases: " + e.Message);
                }
            });
        }

        public void Import<T>(string filePath, IItemCollection<T> db) where T : DatabaseObject
        {
            if (filePath == null)
            {
                return;
            }
            var jsonString = File.ReadAllText(filePath);
            IEnumerable<T> items = JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);

            foreach (var item in items)
            {
                if (!db.Contains(item))
                {
                    db.Add(item);
                }
                else
                {
                    db.Update(item);
                }
            }
        }

        private string SelectJson()
        {
            return playniteApi.Dialogs.SelectFile("json|*.json");
        }

        public RelayCommand SelectGamesCommand
        {
            get => new RelayCommand(() =>
            {
                Settings.Settings.GamesPath = SelectJson();
                Settings.Settings.OnPropertyChanged("gamesPath");
            });
        }

        public RelayCommand SelectGenresCommand
        {
            get => new RelayCommand(() =>
            {
                Settings.Settings.GenresPath = SelectJson();
                Settings.Settings.OnPropertyChanged("genresPath");
            });
        }

        public RelayCommand SelectCategoriesCommand
        {
            get => new RelayCommand(() =>
            {
                Settings.Settings.CategoriesPath = SelectJson();
                Settings.Settings.OnPropertyChanged("categoriesPath");
            });
        }

        public RelayCommand SelectFeaturesCommand
        {
            get => new RelayCommand(() =>
            {
                Settings.Settings.FeaturesPath = SelectJson();
                Settings.Settings.OnPropertyChanged("featuresPath");
            });
        }

        public RelayCommand SelectPlatformCommand
        {
            get => new RelayCommand(() =>
            {
                Settings.Settings.PlatformPath = SelectJson();
                Settings.Settings.OnPropertyChanged("platformPath");
            });
        }

        public RelayCommand SelectRegionsCommand
        {
            get => new RelayCommand(() =>
            {
                Settings.Settings.RegionsPath = SelectJson();
                Settings.Settings.OnPropertyChanged("regionsPath");
            });
        }

        public RelayCommand SelecSeriesCommand
        {
            get => new RelayCommand(() =>
            {
                Settings.Settings.SeriesPath = SelectJson();
                Settings.Settings.OnPropertyChanged("seriesPath");
            });
        }

        public RelayCommand SelecSourcesCommand
        {
            get => new RelayCommand(() =>
            {
                Settings.Settings.SourcesPath = SelectJson();
                Settings.Settings.OnPropertyChanged("sourcesPath");
            });
        }

        public RelayCommand SelecTagsCommand
        {
            get => new RelayCommand(() =>
            {
                Settings.Settings.TagsPath = SelectJson();
                Settings.Settings.OnPropertyChanged("tagsPath");
            });
        }

        public RelayCommand SelecCompletionStatusesCommand
        {
            get => new RelayCommand(() =>
            {
                Settings.Settings.CompletionStatusesPath = SelectJson();
                Settings.Settings.OnPropertyChanged("completionStatusesPath");
            });
        }
    }
}
