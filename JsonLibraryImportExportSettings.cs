using Playnite.SDK;
using Playnite.SDK.Data;
using System.Collections.Generic;

namespace JsonLibraryImportExport
{
    public class JsonLibraryImportExportSettings : ObservableObject
    {
        //EXPORT
        private bool games = false;
        private bool genres = false;
        private bool categories = false;
        private bool features = false;
        private bool platforms = false;
        private bool regions = false;
        private bool series = false;
        private bool sources = false;
        private bool tags = false;
        private bool completionStatuses = false;

        public bool Games { get => games; set => SetValue(ref games, value); }
        public bool Genres { get => genres; set => SetValue(ref genres, value); }
        public bool Categories { get => categories; set => SetValue(ref categories, value); }
        public bool Features { get => features; set => SetValue(ref features, value); }
        public bool Platforms { get => platforms; set => SetValue(ref platforms, value); }
        public bool Regions { get => regions; set => SetValue(ref regions, value); }
        public bool Series { get => series; set => SetValue(ref series, value); }
        public bool Sources { get => sources; set => SetValue(ref sources, value); }
        public bool Tags { get => tags; set => SetValue(ref tags, value); }
        public bool CompletionStatuses { get => completionStatuses; set => SetValue(ref completionStatuses, value); }

        //IMPORT
        private string gamesPath;
        private string genresPath;
        private string categoriesPath;
        private string featuresPath;
        private string platformPath;
        private string regionsPath;
        private string seriesPath;
        private string sourcesPath;
        private string tagsPath;
        private string completionStatusesPath;

        public string GamesPath { get => gamesPath; set => gamesPath = value; }
        public string GenresPath { get => genresPath; set => genresPath = value; }
        public string CategoriesPath { get => categoriesPath; set => categoriesPath = value; }
        public string FeaturesPath { get => featuresPath; set => featuresPath = value; }
        public string PlatformPath { get => platformPath; set => platformPath = value; }
        public string RegionsPath { get => regionsPath; set => regionsPath = value; }
        public string SeriesPath { get => seriesPath; set => seriesPath = value; }
        public string SourcesPath { get => sourcesPath; set => sourcesPath = value; }
        public string TagsPath { get => tagsPath; set => tagsPath = value; }
        public string CompletionStatusesPath { get => completionStatusesPath; set => completionStatusesPath = value; }
    }

    public class JsonLibraryImportExportSettingsViewModel : ObservableObject, ISettings
    {
        private readonly JsonLibraryImportExport plugin;
        private JsonLibraryImportExportSettings editingClone { get; set; }

        private JsonLibraryImportExportSettings settings;
        public JsonLibraryImportExportSettings Settings
        {
            get => settings;
            set
            {
                settings = value;
                OnPropertyChanged();
            }
        }

        public JsonLibraryImportExportSettingsViewModel(JsonLibraryImportExport plugin)
        {
            // Injecting your plugin instance is required for Save/Load method because Playnite saves data to a location based on what plugin requested the operation.
            this.plugin = plugin;

            // Load saved settings.
            var savedSettings = plugin.LoadPluginSettings<JsonLibraryImportExportSettings>();

            // LoadPluginSettings returns null if no saved data is available.
            if (savedSettings != null)
            {
                Settings = savedSettings;
            }
            else
            {
                Settings = new JsonLibraryImportExportSettings();
            }
        }

        public void BeginEdit()
        {
            // Code executed when settings view is opened and user starts editing values.
            editingClone = Serialization.GetClone(Settings);
        }

        public void CancelEdit()
        {
            // Code executed when user decides to cancel any changes made since BeginEdit was called.
            // This method should revert any changes made to Option1 and Option2.
            Settings = editingClone;
        }

        public void EndEdit()
        {
            // Code executed when user decides to confirm changes made since BeginEdit was called.
            // This method should save settings made to Option1 and Option2.
            plugin.SavePluginSettings(Settings);
        }

        public bool VerifySettings(out List<string> errors)
        {
            // Code execute when user decides to confirm changes made since BeginEdit was called.
            // Executed before EndEdit is called and EndEdit is not called if false is returned.
            // List of errors is presented to user if verification fails.
            errors = new List<string>();
            return true;
        }
    }
}