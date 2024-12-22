using JsonLibraryImportExport.View;
using JsonLibraryImportExport.ViewModel;
using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace JsonLibraryImportExport
{
    public class JsonLibraryImportExport : GenericPlugin
    {
        private static readonly ILogger logger = LogManager.GetLogger();

        private JsonLibraryImportExportSettingsViewModel settings { get; set; }

        public override Guid Id { get; } = Guid.Parse("888ab97e-ea1b-40e5-a2da-ef917aee0603");

        public JsonLibraryImportExport(IPlayniteAPI api) : base(api)
        {
            settings = new JsonLibraryImportExportSettingsViewModel(this);
            Properties = new GenericPluginProperties
            {
                HasSettings = true
            };
        }

        public override IEnumerable<MainMenuItem> GetMainMenuItems(GetMainMenuItemsArgs args)
        {
            string menuSection = "@Json Library Import Export";
            return new List<MainMenuItem>
            {
                new MainMenuItem
                {
                    Description = "Open export window",
                    MenuSection = menuSection,
                    Action = _ =>
                    {
                        OpenExportWindow();
                    }
                },
                new MainMenuItem
                {
                    Description = "Open import window",
                    MenuSection = menuSection,
                    Action = _ =>
                    {
                        OpenImportWindow();
                    }
                }
            };
        }

        private void OpenExportWindow()
        {
            var window = PlayniteApi.Dialogs.CreateWindow(new WindowCreationOptions
            {
                ShowMinimizeButton = false,
                ShowMaximizeButton = true
            });
            window.Height = 450;
            window.Width = 250;
            window.Title = "Json Library Export";
            window.Content = new JsonLibraryImportExportView();
            window.DataContext = new JsonLibraryImportExportViewModel(PlayniteApi, settings);
            window.Owner = PlayniteApi.Dialogs.GetCurrentAppWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.ShowDialog();
        }

        private void OpenImportWindow()
        {
            var window = PlayniteApi.Dialogs.CreateWindow(new WindowCreationOptions
            {
                ShowMinimizeButton = false,
                ShowMaximizeButton = true
            });
            window.Height = 550;
            window.Width = 470;
            window.Title = "Json Library Import";
            window.Content = new JsonLibraryImportView();
            window.DataContext = new JsonLibraryImportExportViewModel(PlayniteApi, settings);
            window.Owner = PlayniteApi.Dialogs.GetCurrentAppWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.ShowDialog();
        }

        public override void OnGameInstalled(OnGameInstalledEventArgs args)
        {
            // Add code to be executed when game is finished installing.
        }

        public override void OnGameStarted(OnGameStartedEventArgs args)
        {
            // Add code to be executed when game is started running.
        }

        public override void OnGameStarting(OnGameStartingEventArgs args)
        {
            // Add code to be executed when game is preparing to be started.
        }

        public override void OnGameStopped(OnGameStoppedEventArgs args)
        {
            // Add code to be executed when game is preparing to be started.
        }

        public override void OnGameUninstalled(OnGameUninstalledEventArgs args)
        {
            // Add code to be executed when game is uninstalled.
        }

        public override void OnApplicationStarted(OnApplicationStartedEventArgs args)
        {
            // Add code to be executed when Playnite is initialized.
        }

        public override void OnApplicationStopped(OnApplicationStoppedEventArgs args)
        {
            // Add code to be executed when Playnite is shutting down.
        }

        public override void OnLibraryUpdated(OnLibraryUpdatedEventArgs args)
        {
            // Add code to be executed when library is updated.
        }

        public override ISettings GetSettings(bool firstRunSettings)
        {
            return settings;
        }
    }
}