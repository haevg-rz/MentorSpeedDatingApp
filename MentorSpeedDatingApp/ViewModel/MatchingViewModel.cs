using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MentorSpeedDatingApp.ExtraFunctions;
using MentorSpeedDatingApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MentorSpeedDatingApp.ViewModel
{
    public class MatchingViewModel : ViewModelBase
    {
        #region Fields

        private DateTime startTime;
        private DateTime endTime;
        private List<Mentor> mentors;
        private List<Mentee> mentees;
        private List<Matching> matchings;
        private List<DateSpan> dateTimes;
        private MatchingCalculator matchingCalculator;

        #endregion

        #region Properties

        public DateTime StartTime
        {
            get => this.startTime;
            set => base.Set(ref this.startTime, value);
        }

        public DateTime EndTime
        {
            get => this.endTime;
            set => base.Set(ref this.endTime, value);
        }

        public List<Mentor> Mentors
        {
            get => this.mentors;
            set => base.Set(ref this.mentors, value);
        }

        public List<Mentee> Mentees
        {
            get => this.mentees;
            set => base.Set(ref this.mentees, value);
        }

        public List<Matching> Matchings
        {
            get => this.matchings;
            set
            {
                base.Set(ref this.matchings, value);
                base.RaisePropertyChanged();
            }
        }

        public List<DateSpan> DateTimes
        {
            get => this.dateTimes;
            set => base.Set(ref this.dateTimes, value);
        }

        public class TableElement
        {
            public string Content { get; set; }
            public bool IsMentor { get; set; }
            public bool IsTime { get; set; }
        }

        public List<List<TableElement>> MatchingList { get; set; }

        #endregion

        #region RelayCommands

        public RelayCommand<Visual> PrintCommand { get; set; }

        #endregion

        public MatchingViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.GenerateTestData();
                this.MatchingList = new List<List<TableElement>>
                {
                    new List<TableElement>
                    {
                        new TableElement {Content = "Uhrzeit", IsTime = true, IsMentor = true},
                        new TableElement {Content = "Dr. Lana Grey", IsMentor = true},
                        new TableElement {Content = "Dr. Franziska Hirsch", IsMentor = true},
                        new TableElement {Content = "Prof. Dr. med. Elizabeth von Stuttenhausen", IsMentor = true}
                    },
                    new List<TableElement>
                    {
                        new TableElement {Content = "08:00 - 08:30", IsTime = true},
                        new TableElement {Content = "Lisa Su"},
                        new TableElement {Content = "Dora Beng"},
                        new TableElement {Content = "Nora Ellingdottir"}
                    },
                    new List<TableElement>
                    {
                        new TableElement {Content = "08:30 - 09:00", IsTime = true},
                        new TableElement {Content = "Manuella Beckenbauer-Hülsenman"},
                        new TableElement {Content = "Katharina Lindenborn"},
                        new TableElement {Content = "Siglinde Skjége"}
                    }
                };
            }
        }

        public MatchingViewModel(ObservableCollection<Mentor> mentorsList,
            ObservableCollection<Mentee> menteeList, DateTime startTime, DateTime endTime)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Mentors = mentorsList.ToList();
            this.Mentees = menteeList.ToList();
            this.PrintCommand = new RelayCommand<Visual>(this.PrintCommandHandling);
            this.Initiate();
        }

        private void Initiate()
        {
            this.matchingCalculator =
                new MatchingCalculator(this.StartTime, this.EndTime, this.Mentors, this.Mentees);
            this.Matchings = this.matchingCalculator.Matchings;
            this.DateTimes = this.FormatDateTimeList(this.matchingCalculator.MatchingDates);

            this.MatchingList = new List<List<TableElement>> {new List<TableElement>()};
            this.MatchingList[0].Add(new TableElement {Content = "Uhrzeit", IsMentor = true, IsTime = true});

            foreach (var matching in this.Matchings)
            {
                this.MatchingList[0].Add(new TableElement {Content = matching.Mentor.ToString(), IsMentor = true});
            }

            for (var i = 0; i < this.DateTimes.Count; i++)
            {
                this.MatchingList.Add(new List<TableElement>());
                this.MatchingList[i + 1].Add(new TableElement {Content = this.DateTimes[i].Time, IsTime = true});
            }

            var minLength = this.Matchings.Min(m => m.Dates.Count);
            var maxLength = this.Matchings.Max(m => m.Dates.Count);

            if (minLength != maxLength)
            {
                var toshortMatchings = this.Matchings.Where(m => m.Dates.Count == minLength);
                foreach (var matching in toshortMatchings)
                {
                    matching.Dates.Add(new BreakDate {Mentee = "-"});
                }
            }

            foreach (var matching in this.Matchings)
            {
                var k = 0;
                foreach (var date in matching.Dates)
                {
                    this.MatchingList[1 + k].Add(new TableElement {Content = date.Mentee.ToString()});
                    k++;
                }
            }
        }

        private List<DateSpan> FormatDateTimeList(List<IDate> source)
        {
            var formattedDatesList = new List<DateSpan>();

            if (source.Count > 1)
            {
                for (var i = 0; i < source.Count() - 1; i++)
                {
                    formattedDatesList.Add(new DateSpan
                    {
                        Time =
                            $"{source[i].TimeSlot.Time:HH:mm} - {source[i + 1].TimeSlot.Time:HH:mm}"
                    });
                }
            }

            formattedDatesList.Add(
                new DateSpan
                {
                    Time =
                        $"{source.Last().TimeSlot.Time:HH:mm} - {source.Last().TimeSlot.Time.AddMinutes(this.matchingCalculator.DateDuration):HH:mm}"
                });

            return formattedDatesList;
        }

        #region CommandHandlings

        private void PrintCommandHandling(Visual v)
        {
            if (!(v is FrameworkElement e))
                return;

            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
                var originalScale = e.LayoutTransform;
                PrintCapabilities capabilities =
                    printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
                var scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / e.ActualWidth,
                    capabilities.PageImageableArea.ExtentHeight / e.ActualHeight);
                e.LayoutTransform = new ScaleTransform(scale - 0.05, scale - 0.05);
                var availableSize = new Size(capabilities.PageImageableArea.ExtentWidth,
                    capabilities.PageImageableArea.ExtentHeight);
                e.Measure(availableSize);
                e.Arrange(new Rect(
                    new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight),
                    availableSize));
                printDialog.PrintVisual(e, "MentorSpeedDating_" + DateTime.Now.Date.ToShortDateString());
                e.LayoutTransform = originalScale;
            }
        }

        #endregion

        #region TestGenerators

        private void GenerateTestData()
        {
            this.Mentors = GenerateTestMentors();
            this.Mentees = GenerateTestMentees();
            this.StartTime = new DateTime(2020, 7, 21, 9, 0, 0);
            this.EndTime = new DateTime(2020, 7, 21, 16, 0, 0);
            this.Matchings = this.GenerateTestMatchings();
            this.DateTimes = this.GenerateTestDateTimes();
        }

        private List<DateSpan> GenerateTestDateTimes()
        {
            return new List<DateSpan>
            {
                new DateSpan {Time = "09:00 - 09:45"},
                new DateSpan {Time = "09:00 - 09:45"},
                new DateSpan {Time = "09:00 - 09:45"},
                new DateSpan {Time = "09:00 - 09:45"},
                new DateSpan {Time = "09:00 - 09:45"}
            };
        }

        private List<Matching> GenerateTestMatchings()
        {
            var testMatchings = new List<Matching>();
            foreach (var testMentor in GenerateTestMentors())
            {
                var testMatch = new Matching()
                {
                    Dates = this.GenerateTestDates(),
                    Mentor = testMentor
                };
                testMatchings.Add(testMatch);
            }

            return testMatchings;
        }

        private List<IDate> GenerateTestDates()
        {
            var testDates = new List<IDate>();
            foreach (var testMentee in GenerateTestMentees())
            {
                var date = new Date()
                {
                    Mentee = testMentee,
                    TimeSlot = new TimeSlot()
                    {
                        IsBreak = false,
                        Time = new DateTime(2020, 7, 21, 8, 30, 0).AddMinutes(30)
                    }
                };
                testDates.Add(date);
            }

            return testDates;
        }

        private static List<Mentee> GenerateTestMentees()
        {
            var testMentees = new List<Mentee>()
            {
                new Mentee()
                    {Name = "TestMentee1", Titel = "TestTitel1", Vorname = "TestVorname1", IsFiller = false},
                new Mentee()
                    {Name = "TestMentee2", Titel = "TestTitel2", Vorname = "TestVorname2", IsFiller = false},
                new Mentee()
                    {Name = "TestMentee3", Titel = "TestTitel3", Vorname = "TestVorname3", IsFiller = false},
                new Mentee()
                    {Name = "TestMentee4", Titel = "TestTitel4", Vorname = "TestVorname4", IsFiller = false},
                new Mentee()
                    {Name = "TestMentee5", Titel = "TestTitel5", Vorname = "TestVorname5", IsFiller = false}
            };
            return testMentees;
        }

        private static List<Mentor> GenerateTestMentors()
        {
            var testMentors = new List<Mentor>()
            {
                new Mentor() {Name = "TestMentor1", Titel = "TestTitel1", Vorname = "TestVorname1"},
                new Mentor() {Name = "TestMentor2", Titel = "TestTitel2", Vorname = "TestVorname2"},
                new Mentor() {Name = "TestMentor3", Titel = "TestTitel3", Vorname = "TestVorname3"},
                new Mentor() {Name = "TestMentor4", Titel = "TestTitel4", Vorname = "TestVorname4"},
                new Mentor() {Name = "TestMentor5", Titel = "TestTitel5", Vorname = "TestVorname5"}
            };
            return testMentors;
        }

        #endregion
    }
}