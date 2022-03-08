using StandUpMate.Command;
using System.Windows;
using System.Windows.Input;

namespace StandUpMate.ViewModel
{
    internal class NotifyIconViewModel
    {
        public Window SettingsWindow { get; set; }

        /// <summary>
        /// Shows the settings window if not opened yet.
        /// </summary>
        public ICommand ShowSettingsWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => SettingsWindow == null,
                    CommandAction = () =>
                    {
                        SettingsWindow = new MainWindow();
                        SettingsWindow.Show();
                    }
                };
            }
        }

        /// <summary>
        /// Hides the settings window if opened.
        /// </summary>
        public ICommand HideSettingsWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => SettingsWindow != null,
                    CommandAction = () =>
                    {
                        SettingsWindow.Close();
                        SettingsWindow = null;
                    }
                };
            }
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() };
            }
        }
    }
}