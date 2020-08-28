using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.ViewModel;
using MentorSpeedDatingApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MentorSpeedDatingApp.WindowManagement
{
    public class WindowManager
    {
        private static ShowNoGoDatesWindow noDatesWindow;
        private static List<MatchingWindow> matchingWindows = new List<MatchingWindow>();

        public static void ShowMatchingWindow(MainViewModel mVm, bool useNoGoDates)
        {
            var window = new MatchingWindow();
            window.DataContext =
                new MatchingViewModel(mVm.Mentors, mVm.Mentees, mVm.StartTime, mVm.EndTime, mVm.Headline, mVm.NoGoDates,
                    useNoGoDates);
            window.Show();
            matchingWindows.Add(window);
        }

        public static void ShowNoGoDatesWindow(ObservableCollection<(Mentor, Mentee)> noGoDates)
        {
            if (noDatesWindow != null && Application.Current.Windows.OfType<ShowNoGoDatesWindow>().Any())
            {
                noDatesWindow.Close();
            }

            noDatesWindow = new ShowNoGoDatesWindow {DataContext = new ShowNoGoDatesViewModel(noGoDates)};
            noDatesWindow.Show();
        }

        public static void CloseAllWindows()
        {
            noDatesWindow?.Close();

            foreach (var matchingWindow in matchingWindows)
            {
                matchingWindow.Close();
            }
        }
    }
}