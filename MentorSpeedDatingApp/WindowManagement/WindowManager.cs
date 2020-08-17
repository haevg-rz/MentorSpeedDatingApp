using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.ViewModel;
using MentorSpeedDatingApp.Views;
using System.Collections.Generic;

namespace MentorSpeedDatingApp.WindowManagement
{
    public class WindowManager
    {
        public static void ShowMatchingWindow(MainViewModel mVm)
        {
            var window = new MatchingWindow();
            window.DataContext =
                new MatchingViewModel(mVm.Mentors, mVm.Mentees, mVm.StartTime, mVm.EndTime, mVm.Headline, mVm.NoGoDates);
            window.Show();
        }

        public static void ShowNoGoDatesWindow(List<(Mentor,Mentee)> noGoDates)
        {
            var window = new ShowNoGoDatesWindow();
            window.DataContext = new ShowNoGoDatesViewModel(noGoDates);
            window.Show();
        }
    }
}