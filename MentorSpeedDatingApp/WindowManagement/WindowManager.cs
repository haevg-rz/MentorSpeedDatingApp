using MentorSpeedDatingApp.ExtraFunctions;
using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.ViewModel;
using MentorSpeedDatingApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
                    "Die Ausgabemenge ist zu gro� f�r eine Druckseite. Im Hintergrund haben sich Fenster ge�ffnet, die Sie einzeln drucken k�nnen.",
                    "Warnung");

                var firstWindow = new MatchingWindow
                {
                    DataContext = new MatchingViewModel(calculator, calculator.Matchings,
                        mVm.Headline + " Gesamt�bersicht", true)
                };
                firstWindow.Show();
                matchingWindows.Add(firstWindow);

                for (var index = 0; index <= listOfSplitMatchings.Count - 1; index++)
                {
                    var seitenAnzahl = index + 1;
                    var seitenLabel = mVm.Headline + " - Seite " + seitenAnzahl;
                    var submatchings = listOfSplitMatchings[index];
                    var window = new MatchingWindow
                    {
                        DataContext = new MatchingViewModel(calculator, submatchings, seitenLabel, false),
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
                    DataContext = new MatchingViewModel(calculator, calculator.Matchings, mVm.Headline, true)
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