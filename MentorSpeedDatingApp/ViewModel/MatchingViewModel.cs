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

        public List<Matching> Matchings
        {
            get => this.matchings;
            set
            {
                base.Set(ref this.matchings, value);
                base.RaisePropertyChanged();
            }
        }

        public List<DateTime> Times
        {
            get => this.times;
            set
            {
                base.Set(ref this.times, value);
                base.RaisePropertyChanged();
            }
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

        public DateTime Date
        {
            get => this.date;
            set => base.Set(ref this.date, value);
        }

        public ObservableCollection<Mentor> Mentors
        {
            get => this.mentors;
            set => base.Set(ref this.mentors, value);
        }

        public List<DateTime> TimeSlots
        {
            get => this.timeSlots;
            set => base.Set(ref this.timeSlots, value);
        }

        public ObservableCollection<Mentee> Mentees
        {
            get => this.mentees;
            set => base.Set(ref this.mentees, value);
        }

        #region Backing Fields

        private List<Mentor> mentors;
        private List<Mentee> mentees;
        private List<Matching> matchings;
        private List<DateTime> times;

        #endregion

        #endregion

        public MatchingViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.Mentors = new List<Mentor>()
                {
                    new Mentor() {Name = "TestMentor1", Vorname = "TestMentor1Vorname", Titel = "TestTitel1"},
                    new Mentor() {Name = "TestMentor2", Vorname = "TestMentor2Vorname", Titel = "TestTitel2"},
                    new Mentor() {Name = "TestMentor3", Vorname = "TestMentor3Vorname", Titel = "TestTitel3"},
                    new Mentor() {Name = "TestMentor4", Vorname = "TestMentor4Vorname", Titel = "TestTitel4"},
                    new Mentor() {Name = "TestMentor5", Vorname = "TestMentor5Vorname", Titel = "TestTitel5"},
                };
                this.Mentees = new List<Mentee>()
                {
                    new Mentee() {Name = "TestMentee1", Vorname = "TestMentee1Vorname", Titel = "TestTitel1"},
                    new Mentee() {Name = "TestMentee2", Vorname = "TestMentee2Vorname", Titel = "TestTitel2"},
                    new Mentee() {Name = "TestMentee3", Vorname = "TestMentee3Vorname", Titel = "TestTitel3"},
                    new Mentee() {Name = "TestMentee4", Vorname = "TestMentee4Vorname", Titel = "TestTitel4"},
                    new Mentee() {Name = "TestMentee5", Vorname = "TestMentee5Vorname", Titel = "TestTitel5"},
                    new Mentee() {Name = "TestMentee6", Vorname = "TestMentee6Vorname", Titel = "TestTitel6"}
                };
                this.Times = new List<DateTime>();
                var startTime = DateTime.Today;
                for (int i = 0; i < 8; i++)
                {
                    this.Times.Add(startTime.AddHours(i));
                }
                this.Matchings = new List<Matching>();
                foreach (var mentor in this.Mentors)
                {
                    this.Matchings.Add(new Matching()
                    {
                        Mentor = mentor,
                        Dates = this.GenerateTestDates()
                    });
                }
            }
        }

        public MatchingViewModel(string headline, ObservableCollection<Mentor> mentorsList,
            ObservableCollection<Mentee> menteeList, DateTime startTime, DateTime endTime)
        {
            this.Headline = headline;
            this.Mentors = mentorsList;
            this.Mentees = menteeList;
            this.Mentors = mentorsList;
        }

        private List<IDate> GenerateTestDates()
        {
            List<IDate> dates = new List<IDate>();
            foreach (var mentee in this.Mentees)
            {
                var ts = DateTime.Now;
                var addHours = ts.AddHours(1);
                dates.Add(new Date()
                {
                    Mentee = mentee,
                    TimeSlot = new TimeSlot() { IsBreak = false, Time = addHours}
                });
            }

            return dates;
        }
    }
}