using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Timers;

namespace StandUpMate.Utility
{
    public class StanceTimer
    {
        private readonly Timer _timer = new Timer();
        private int _minutes;
        private DateTime _startTime;
        private DateTime _endTime;

        public Timer Timer { get => _timer; }

        public void StartTimer()
        {
            _minutes = Properties.Settings.Default.Timer;
            _startTime = DateTime.Now;
            _endTime = _startTime.AddMinutes(_minutes);

            _timer.Interval = TimeSpan.FromMinutes(_minutes).TotalMilliseconds;
            _timer.AutoReset = false;
            _timer.Elapsed += Timer_Tick;
            _timer.Enabled = true;
        }

        public void StopTimer()
        {
            _timer.Enabled = false;
        }

        public string GetRemainingTime()
        {
            TimeSpan remainingTime = _endTime - DateTime.Now;
            return remainingTime.ToString("mm':'ss");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            NotifyIcon.TrayIcon.ShowBalloonTip("Time has expired!", "Please change your stance.", BalloonIcon.Info);
            _timer.Enabled = false;
        }
    }
}