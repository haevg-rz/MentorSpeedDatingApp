using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using MentorSpeedDatingApp.Models;
using  MentorSpeedDatingApp.ExtraFunctions;

namespace MentorSpeedDatingApp.ViewModel
{
    public class MatchingViewModel : ViewModelBase
    {
        #region Properties

        public DataTable MatchingDataTable
        {
            get => this.matchingDataTable;
            set => base.Set(ref this.matchingDataTable, value);
        }

        public string Headline
        {
            get => this.headline;
            set => base.Set(ref this.headline, value);
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

        private string headline;
        private ObservableCollection<Mentor> mentors;
        private ObservableCollection<Mentee> mentees;
        private List<DateTime> timeSlots;
        private DateTime date;
        private DataTable matchingDataTable;

        #endregion

        #endregion


        public MatchingViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.Headline = "Speed Dating 2020 - Übersicht";
                this.Date = DateTime.Now;
            }
        }

        public MatchingViewModel(string headline, ObservableCollection<Mentor> mentorsList,
            ObservableCollection<Mentee> menteeList, DateTime startTime, DateTime endTime)
        {
            this.Headline = headline;
            this.Mentors = mentorsList;
            this.Mentees = menteeList;
            //Fill MacthedMentees for every Mentor with every Mentee (No No-Go-Dates yet implemented)
            this.FillMatchedMenteesProperty(this.Mentees.ToList());
            var tsc = new TimeSlotCalculator();
            var minDates = this.Mentors[0].MatchedMentees.Count;
            this.TimeSlots = tsc.CalculateTimeSlots(startTime, endTime, minDates);
            //Fill DataTable with Lists
            var mdtg = new MatchingDataTableGenerator();
            this.MatchingDataTable = mdtg.GenerateTable(this.Mentors.ToList(), this.TimeSlots);
        }

        private void FillMatchedMenteesProperty(List<Mentee> menteeList)
        {
            foreach (var mentor in this.Mentors)
            {
                mentor.MatchedMentees = menteeList;
            }
        }
    }
}