using Hardcodet.Wpf.TaskbarNotification;
using StandUpMate.Command;
using StandUpMate.Utility;
using System.Windows;
using System.Windows.Input;

namespace StandUpMate.ViewModel
{
    public class NotifyIconViewModel
    {
        private StanceTimer _stanceTimer = new StanceTimer();
        public Window SettingsWindow { get; set; }

        /// <summary>
        /// Starts timer or shows remaining time.
        /// </summary>
        public ICommand LeftClickCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => true,
                    CommandAction = () =>
                    {
                        if (_stanceTimer.Timer.Enabled)
                        {
                            ShowRemainingTimeAsBaloon();
                        }
                        else
                        {
                            _stanceTimer.StartTimer();
                        }
                    }
                };
            }
        }

        /// <summary>
        /// Shows the settings window if not opened yet.
        /// </summary>
        public ICommand ShowSettingsWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null || Application.Current.MainWindow.IsActive == false,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow = new MainWindow();
                        Application.Current.MainWindow.Show();
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
                    CanExecuteFunc = () => Application.Current.MainWindow.IsActive == true,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow.Close();
                    }
                };
            }
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public ICommand StartTimerCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => _stanceTimer.Timer.Enabled == false,
                    CommandAction = () =>
                    {
                        _stanceTimer.StartTimer();
                    }
                };
            }
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public ICommand StopTimerCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => _stanceTimer.Timer.Enabled == true,
                    CommandAction = () =>
                    {
                        _stanceTimer.StopTimer();
                    }
                };
            }
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public ICommand ShowRemainingTimeCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => _stanceTimer.Timer.Enabled == true,
                    CommandAction = () =>
                    {
                        ShowRemainingTimeAsBaloon();
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

        /// <summary>
        /// Shows the remaining time as BaloonTip.
        /// </summary>
        private void ShowRemainingTimeAsBaloon()
        {
            var title = "Timer";
            var message = $"Remaining time: {_stanceTimer.GetRemainingTime()}";
            NotifyIcon.TrayIcon.ShowBalloonTip(title, message, BalloonIcon.Info);
        }
    }
}