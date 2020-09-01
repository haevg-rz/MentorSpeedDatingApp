using GalaSoft.MvvmLight;
using MentorSpeedDatingApp.ExtraFunctions;
using MentorSpeedDatingApp.Models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight.CommandWpf;
using Point = System.Windows.Point;

namespace MentorSpeedDatingApp.ViewModel
{
    public class MatchingViewModel : ViewModelBase
    {
        #region Fields

/*        private DateTime startTime;
        private DateTime endTime;
        private List<Mentor> mentors;
        private List<Mentee> mentees;*/
        private List<Matching> matchings;
        private List<DateSpan> dateTimes;
        private MatchingCalculator matchingCalculator;
        private string headline;

        #endregion

        #region Properties

/*
        public DateTime StartTime
        {
            get => this.startTime;
            set => base.Set(ref this.startTime, value);
        }
*/

/*        public DateTime EndTime
        {
            get => this.endTime;
            set => base.Set(ref this.endTime, value);
        }*/

/*        public List<Mentor> Mentors
        {
            get => this.mentors;
            set => base.Set(ref this.mentors, value);
        }*/

/*        public List<Mentee> Mentees
        {
            get => this.mentees;
            set => base.Set(ref this.mentees, value);
        }*/

        public List<Matching> Matchings
        {
            get => this.matchings;
            set
            {
                base.Set(ref this.matchings, value);
                base.RaisePropertyChanged();
            }
        }

        public ObservableCollection<(Mentor, Mentee)> NoGoDates { get; set; }

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

        public String Headline
        {
            get => this.headline;
            set => base.Set(ref this.headline, value);
        }

        #endregion

        #region RelayCommands

        public GalaSoft.MvvmLight.Command.RelayCommand<Visual> PrintCommand { get; set; }
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand ExportCommand { get; }

        #endregion

        public MatchingViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.Headline = "Mentor Speed Dating";
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

        public MatchingViewModel(MatchingCalculator calc, List<Matching> matchings, string headline)
        {
            this.ExportCommand = new RelayCommand(this.ExportCommandHandling);
            this.PrintCommand = new GalaSoft.MvvmLight.Command.RelayCommand<Visual>(this.PrintCommandHandling);
            this.Headline = String.IsNullOrWhiteSpace(headline) ? "Mentor Speed Dating" : headline;
            this.matchingCalculator = calc;
            this.Matchings = matchings;
            this.Initiate();
        }

        private void Initiate()
        {
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

            while (minLength != maxLength)
            {
                var toShortMatchings = this.Matchings.Where(m => m.Dates.Count != maxLength);
                foreach (var matching in toShortMatchings)
                {
                    matching.Dates.Add(new BreakDate {Mentee = "-"});
                }

                minLength = this.Matchings.Min(m => m.Dates.Count);
                maxLength = this.Matchings.Max(m => m.Dates.Count);
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
                if (capabilities.PageImageableArea != null)
                {
                    var scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / e.ActualWidth,
                        capabilities.PageImageableArea.ExtentHeight / e.ActualHeight);
                    e.LayoutTransform = new ScaleTransform(scale - 0.05, scale - 0.05);
                }

                if (capabilities.PageImageableArea != null)
                {
                    var availableSize = new Size(capabilities.PageImageableArea.ExtentWidth,
                        capabilities.PageImageableArea.ExtentHeight);
                    e.Measure(availableSize);
                    e.Arrange(new Rect(
                        new Point(capabilities.PageImageableArea.OriginWidth,
                            capabilities.PageImageableArea.OriginHeight),
                        availableSize));
                }

                printDialog.PrintVisual(e, "MentorSpeedDating_" + DateTime.Now.Date.ToShortDateString());
                e.LayoutTransform = originalScale;
            }
        }

        private void ExportCommandHandling()
        {
            var excel = new Microsoft.Office.Interop.Excel.Application
            {
                DisplayAlerts = false
            };
            var workBook = excel.Workbooks.Add("");

            var lenghOfTimeSlot =
                Matchings[0].Dates[1].TimeSlot.Time.Minute - Matchings[0].Dates[0].TimeSlot.Time.Minute;

            _Worksheet sheet = (_Worksheet) workBook.ActiveSheet;

            try
            {
                for (int i = 0; i < this.MatchingList.Count; i++)
                {
                    for (int j = 0; j < this.MatchingList[i].Count; j++)
                    {
                        sheet.Cells[i + 2, j + 1] = this.MatchingList[i][j].Content.Trim();
                    }
                }

                sheet.Range["A1", "Z2"].EntireRow.Font.Bold = true;
                sheet.Range["A1", "Z2"].EntireRow.VerticalAlignment = XlVAlign.xlVAlignCenter;
                sheet.Range["A1", "A9"].EntireColumn.Font.Bold = true;
                sheet.Range["A1", "A9"].EntireColumn.VerticalAlignment = XlVAlign.xlVAlignCenter;
                sheet.Range["A1", "Z2"].EntireColumn.AutoFit();
                var title = string.IsNullOrWhiteSpace(this.headline)
                    ? "SpeedDateMatching.xlsx"
                    : this.headline + ".xlsx";
                var outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "MSDAPP", title);

                workBook.SaveAs(outputPath,
                    XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false,
                    false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing,
                    Type.Missing, Type.Missing,
                    Type.Missing);
            }
            finally
            {
                workBook.Close();
                excel.Quit();
                Process.Start("explorer.exe", Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "MSDAPP"));
            }
        }

        #endregion
    }
}