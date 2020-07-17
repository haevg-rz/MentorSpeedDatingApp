using System;
using System.Collections.ObjectModel;
using MentorSpeedDatingApp.Views;
using MentorSpeedDatingApp.ViewModel;

namespace MentorSpeedDatingApp.WindowManagement
{
    public class WindowManager
    {
        public static void ShowMatchingWindow(MainViewModel mVm)
        {
            //TESTDATETIME DATA:
            DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0,0);
            DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 0,0);
            var window = new MatchingWindow() {DataContext = new MatchingViewModel(mVm.Mentors, mVm.Mentees, startTime, endTime)};
            window.Show();
        }
    }
}