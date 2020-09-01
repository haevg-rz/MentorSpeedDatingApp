using System;
using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.ViewModel;
using MentorSpeedDatingApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using MentorSpeedDatingApp.ExtraFunctions;

namespace MentorSpeedDatingApp.WindowManagement
{
    public class WindowManager
    {
        private static ShowNoGoDatesWindow noDatesWindow;
        private static List<MatchingWindow> matchingWindows = new List<MatchingWindow>();

        public static void ShowMatchingWindow(MainViewModel mVm, bool useNoGoDates)
        {
            List<Mentor> mentorList = mVm.Mentors.ToList();
            List<Mentee> menteeList = mVm.Mentees.ToList();
            var calculator = new MatchingCalculator(mVm.StartTime, mVm.EndTime, mentorList, menteeList, mVm.NoGoDates, useNoGoDates);
            var matchings = calculator.Matchings;
            var dates = calculator.MatchingDates;
            List<List<Matching>> listOfMatchings = SplitMatchingsIntoSmallerMatchings(matchings, 8);
            if (matchings.Count > 8)
            {
                MessageBox.Show(
                    "Die Ausgabemenge ist zu groß für eine Druckseite. Im Hintergrund haben sich Fenster geöffnet, die Sie einzeln drucken können.", "Warnung");

                var firstWindow = new MatchingWindow();
                firstWindow.DataContext = new MatchingViewModel(calculator, calculator.Matchings, mVm.Headline);
                firstWindow.Show();
                matchingWindows.Add(firstWindow);

                for (var index = 0; index < listOfMatchings.Count; index++)
                {
                    var seitenLabel = mVm.Headline + " - Seite " + index;
                    var m = listOfMatchings[index];
                    var window = new MatchingWindow();
                    window.DataContext =
                        new MatchingViewModel(calculator, m, seitenLabel);
                    window.WindowState = WindowState.Minimized;

                    window.Show();
                    matchingWindows.Add(window);
                }
            }
            else
            {
                var window = new MatchingWindow();
                window.DataContext =
                    new MatchingViewModel(calculator, calculator.Matchings, mVm.Headline);
                window.Show();
                matchingWindows.Add(window);
            }

        }

        private static List<List<Matching>> SplitMatchingsIntoSmallerMatchings(List<Matching> matchings, int size)
        {
            var outerList = new List<List<Matching>>();
            for (int i = 0; i < matchings.Count; i+=size)
            {
                outerList.Add(matchings.GetRange(i, Math.Min(size, matchings.Count - i)));
            }

            return outerList;
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