using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TaskAppT2._Models;
using TaskAppT2._Views.Timer;

namespace TaskAppT2._ViewModels.Timer
{
    partial class TimerPageVM : ObservableObject
    {
        public TimerPageVM(LocalDataBase db)
        {
            this.db = db;
            var patt = db.IntervalPatterns.FirstOrDefault();
            if (patt != null)
            {
                Intervals = patt.Intervals.ToObservableCollection();
            }
            else
            {
                Intervals = [];
                AddIntervals();
            }
            selectInterval = Intervals[0];
        }

        [ObservableProperty]
        public partial ObservableCollection<Interval> Intervals { get; set; }

        [ObservableProperty]
        public partial string TimeText { get; set; } = "0:0";

        readonly LocalDataBase db;
        System.Timers.Timer timer = new System.Timers.Timer();
        DateTime time;
        static float TIMER_INTERVAL = 1000f;
        Interval? selectInterval;
        EndIntervalModalPage? endModalPage;

        [RelayCommand]
        void StartTimer()
        {
            timer.Interval = TIMER_INTERVAL;
            timer.Elapsed += UpdateTimer;
            timer.Start();
        }

        [RelayCommand]
        void StopTimer()
        {
            timer.Stop();
            timer.Elapsed -= UpdateTimer;
        }

        [RelayCommand]
        void DropTimer()
        {
            timer.Stop();
            time = new DateTime();
            selectInterval = Intervals.FirstOrDefault();
            timer.Elapsed -= UpdateTimer;
            TimeText = time.Minute + ":" + time.Second;
        }

        private void UpdateTimer(object? sender, ElapsedEventArgs e)
        {
            time = time.AddMilliseconds(TIMER_INTERVAL);
            TimeText = time.Minute + ":" + time.Second;
            if (selectInterval != null && time.Minute >= selectInterval.Minutes && time.Second >= selectInterval.Seconds)
            {
                int index = Intervals.IndexOf(selectInterval);
                if (index < Intervals.Count - 1)
                {
                    selectInterval = Intervals[index + 1];
                    time = new DateTime();
                    StopTimer();
                    Shell.Current.Navigation.PushModalAsync(endModalPage ??= new EndIntervalModalPage());
                }
                else 
                {
                    StopTimer();
                    Shell.Current.Navigation.PushModalAsync(endModalPage ??= new EndIntervalModalPage());
                }
            }
        }

        [RelayCommand]
        void ClosePanEndInterval()
        {
            Shell.Current.Navigation.PopModalAsync();
            StartTimer();
        }

        [RelayCommand]
        void AddIntervals()
        {
            Intervals.Add(new Interval(IntervalType.Job));
            Intervals.Add(new Interval(IntervalType.Relax));
            var interv = db.IntervalPatterns.FirstOrDefault();
            if (interv == null)
            {
                db.IntervalPatterns.Add(new IntervalPattern("base") { Intervals = [.. Intervals] });
            }
            else
            {
                interv.Intervals = [..  Intervals];
            }
            db.SaveChangesAsync();
        }

        [RelayCommand]
        void RemoveIntervals()
        {
            if (Intervals.Count >= 2)
            {
                Intervals.RemoveAt(Intervals.Count - 1);
                Intervals.RemoveAt(Intervals.Count - 1);
                db.SaveChangesAsync();
            }
        }
    }
}
