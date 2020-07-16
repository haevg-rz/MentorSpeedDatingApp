using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.ExtraFunctions;

namespace MentorSpeedDatingApp.ViewModel
{
    public class MatchingViewModel : ViewModelBase
    {
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

        public List<IDate> DateTimes { get => this.dateTimes; set => base.Set(ref this.dateTimes, value); }


        #region Backing Fields

        private DateTime startTime;
        private DateTime endTime;
        private List<Mentor> mentors;
        private List<Mentee> mentees;
        private List<Matching> matchings;
        private List<IDate> dateTimes;

        #endregion

        #endregion

        public MatchingViewModel()
        {
            if (IsInDesignMode)
            {
                this.GenerateTestData();
            }
        }

        public MatchingViewModel(ObservableCollection<Mentor> mentorsList,
            ObservableCollection<Mentee> menteeList, DateTime startTime, DateTime endTime)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Mentors = mentorsList.ToList();
            this.Mentees = menteeList.ToList();
            this.Initiate();
        }

        private void Initiate()
        {
            MatchingCalculator matchingCalculator =
                new MatchingCalculator(this.StartTime, this.EndTime, this.Mentors, this.Mentees);
            this.Matchings = matchingCalculator.Matchings;
            this.DateTimes = matchingCalculator.MatchingDates;
        }

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

        private List<IDate> GenerateTestDateTimes()
        {
            var testDateTimes = new List<IDate>()
            {
                new Date()
                {
                    Mentee = "", TimeSlot = new TimeSlot()
                        {IsBreak = false, Time = new DateTime(2020, 7, 21, 9,0,0)},                    

                },
                new Date()
                {
                    Mentee = "", TimeSlot = new TimeSlot()
                        {IsBreak = false, Time = new DateTime(2020, 7, 21, 10,0,0)},

                },
                new Date()
                {
                    Mentee = "", TimeSlot = new TimeSlot()
                        {IsBreak = false, Time = new DateTime(2020, 7, 21,11,0,0)},

                },
                new Date()
                {
                    Mentee = "", TimeSlot = new TimeSlot()
                        {IsBreak = false, Time = new DateTime(2020, 7, 21, 12,0,0)},

                },
                new Date()
                {
                    Mentee = "", TimeSlot = new TimeSlot()
                        {IsBreak = false, Time = new DateTime(2020, 7, 21, 13,0,0)}

                }

            };
            
            return testDateTimes;
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