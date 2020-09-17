using MentorSpeedDatingApp.ExtraFunctions;
using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.ViewModel;
using MentorSpeedDatingApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MentorSpeedDatingApp.WindowManagement
{
    public class WindowManager
    {
        private static ShowNoGoDatesWindow noDatesWindow;
        private static List<MatchingWindow> matchingWindows = new List<MatchingWindow>();

        public static void ShowMatchingWindow(MainViewModel mVm, bool useNoGoDates, int maxDruckSpalten = 8)
        {
            var calculator = new MatchingCalculator(mVm.StartTime, mVm.EndTime, mVm.Mentors.ToList(),
                mVm.Mentees.ToList(), mVm.NoGoDates, useNoGoDates);
            var matchings = calculator.Matchings;
            List<List<Matching>> listOfSplitMatchings = SplitMatchingsIntoSmallerMatchings(matchings, maxDruckSpalten);

            if (matchings.Count > maxDruckSpalten)
            {
                MessageBox.Show(
                    "Die Ausgabemenge ist zu groß für eine Druckseite. Im Hintergrund haben sich Fenster geöffnet, die Sie einzeln drucken können.",
                    "Warnung");

                var firstWindow = new MatchingWindow
                {
                    DataContext = new MatchingViewModel(calculator, calculator.Matchings,
                        mVm.Headline + " Gesamtübersicht", 0, true)
                };
                firstWindow.Show();
                matchingWindows.Add(firstWindow);

                for (var index = 0; index <= listOfSplitMatchings.Count - 1; index++)
                {
                    var seitenAnzahl = index + 1;
                    var submatchings = listOfSplitMatchings[index];
                    var window = new MatchingWindow
                    {
                        DataContext = new MatchingViewModel(calculator, submatchings, mVm.Headline, seitenAnzahl, false),
                        WindowState = WindowState.Minimized
                    };

                    window.Show();
                    matchingWindows.Add(window);
                }
            }
            else
            {
                var window = new MatchingWindow
                {
                    DataContext = new MatchingViewModel(calculator, calculator.Matchings, mVm.Headline, 0, true)
                };
                window.Show();
                matchingWindows.Add(window);
            }
        }

        public static List<List<Matching>> SplitMatchingsIntoSmallerMatchings(List<Matching> matchings, int size)
        {
            var outerList = new List<List<Matching>>();
            for (var i = 0; i < matchings.Count; i += size)
            {
                outerList.Add(matchings.GetRange(i, Math.Min(size, matchings.Count - i)));
            }

            return outerList;
        }

        [ExcludeFromCodeCoverage]
        public static void ShowNoGoDatesWindow(ObservableCollection<(Mentor, Mentee)> noGoDates)
        {
            if (noDatesWindow != null && Application.Current.Windows.OfType<ShowNoGoDatesWindow>().Any())
            {
                noDatesWindow.Close();
            }

            noDatesWindow = new ShowNoGoDatesWindow {DataContext = new ShowNoGoDatesViewModel(noGoDates)};
            noDatesWindow.Show();
        }

        [ExcludeFromCodeCoverage]
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