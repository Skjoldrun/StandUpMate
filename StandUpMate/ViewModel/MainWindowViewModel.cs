using StandUpMate.Command;
using StandUpMate.Utility;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace StandUpMate.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        public string TimerSetting { get; set; } = Properties.Settings.Default.Timer.ToString();

        /// <summary>
        /// Saves the new given timer setting, enabled if it is validated as digit characters only
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => NumberValidation(TimerSetting),
                    CommandAction = () =>
                    {
                        Properties.Settings.Default.Timer = int.Parse(TimerSetting);
                        Properties.Settings.Default.Save();
                        Application.Current.MainWindow.Close();
                    }
                };
            }
        }

        /// <summary>
        /// Aborts and closes the settings window.
        /// </summary>
        public ICommand AbortCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => true,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow.Close();
                    }
                };
            }
        }

        /// <summary>
        /// Checks match [0-9] and other digit characters.
        /// </summary>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        public bool NumberValidation(string toCheck)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(toCheck);
        }
    }
}