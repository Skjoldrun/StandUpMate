using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;

namespace StandUpMate
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");

            // Upgrades the USersettings after upgradings the app version
            if (StandUpMate.Properties.Settings.Default.SettingsUpgradeRequired)
            {
                StandUpMate.Properties.Settings.Default.Upgrade();
                StandUpMate.Properties.Settings.Default.SettingsUpgradeRequired = false;
                StandUpMate.Properties.Settings.Default.Save();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}