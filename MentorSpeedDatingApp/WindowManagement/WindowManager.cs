using System.Collections.ObjectModel;
using MentorSpeedDatingApp.Views;
using MentorSpeedDatingApp.ViewModel;

namespace MentorSpeedDatingApp.WindowManagement
{
    public class WindowManager
    {
        public static void ShowMatchingWindow()
        {
            var window = new MatchingWindow() {DataContext = new MatchingViewModel()};
            window.Show();
        }
    }
}