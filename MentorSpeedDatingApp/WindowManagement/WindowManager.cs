using MentorSpeedDatingApp.ViewModel;
using MentorSpeedDatingApp.Views;

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
    }
}