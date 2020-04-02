using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.ViewModel;
using Xunit;

namespace MentorSpeedDatingAppTest
{
    public class TestMainViewModel
    {
        #region Setup

        MainViewModel mv = new MainViewModel();

        public ObservableCollection<Mentee> GenerateMentees()
        {
            var mentees = new ObservableCollection<Mentee>();
            mentees.Add(new Mentee() {Name = "Portman", Titel = "Dr.", Vorname = "Natalie"});
            mentees.Add(new Mentee() {Name = "Theron", Titel = "Prof.", Vorname = "Charlize"});
            mentees.Add(new Mentee() {Name = "Watson", Titel = "Dr.", Vorname = "Emma"});
            mentees.Add(new Mentee() {Name = "Roberts", Titel = "Dr. med.", Vorname = "Julia"});
            return mentees;
        }

        public ObservableCollection<Mentor> GenerateMentors()
        {
            var mentors = new ObservableCollection<Mentor>();
            mentors.Add(new Mentor() {Name = "Bullock", Titel = "", Vorname = "Sandra"});
            mentors.Add(new Mentor() {Name = "Aniston", Titel = "Prof. Dr.", Vorname = "Jennifer"});
            mentors.Add(new Mentor() {Name = "Johansson", Titel = "Dr.", Vorname = "Scarlett"});
            mentors.Add(new Mentor() {Name = "Streep", Titel = "Prof.", Vorname = "Meryl"});
            return mentors;
        }

        #endregion

        [Fact]
        public void Test_DeleteAll_Mentors()
        {
            #region Arange

            this.mv.Mentors = this.GenerateMentors();

            #endregion

            #region Act

            this.mv.DeleteMentorsCommandHandling();

            #endregion

            #region Assert

            Assert.Empty(this.mv.Mentors);

            #endregion
        }        

        [Fact]
        public void Test_DeleteAll_Mentees()
        {
            #region Arange

            this.mv.Mentees = this.GenerateMentees();

            #endregion

            #region Act

            this.mv.DeleteMenteesCommandHandling();

            #endregion

            #region Assert

            Assert.Empty(this.mv.Mentees);

            #endregion
        }

        [Fact]
        public void Test_DeleteAllDataCommandHandling()
        {
            #region Arange

            this.mv.StartTimeHours = "12";
            this.mv.StartTimeMinutes = "00";
            this.mv.EndTimeHours = "18";
            this.mv.EndTimeMinutes = "00";
            this.mv.Date = DateTime.Now;
            this.mv.Mentors = this.GenerateMentors();
            this.mv.Mentees = this.GenerateMentees();

            #endregion

            #region Act

            this.mv.DeleteAllDataCommandHandling();

            #endregion

            #region Assert

            Assert.Empty(this.mv.StartTimeHours);
            Assert.Empty(this.mv.StartTimeMinutes);
            Assert.Empty(this.mv.EndTimeHours);
            Assert.Empty(this.mv.EndTimeMinutes);
            //Bitte nicht um 0:00 ausführen...
            Assert.Equal(DateTime.Now.Date, this.mv.Date.Date);
            Assert.Empty(this.mv.Mentors);
            Assert.Empty(this.mv.Mentees);

            #endregion

        }

        #region TestCaseAttributes
        [Theory]
        [InlineData(false, false, false, false, true)]
        [InlineData(false, true, false, false, false)]
        [InlineData(false, true, true, false, false)]
        [InlineData(false, true, true, true, false)]
        [InlineData(false, false, true, false, false)]
        [InlineData(false, false, true, true, false)]
        [InlineData(false, false, false, true, false)]
        [InlineData(true, true, true, true, false)]
        #endregion
        public void Test_CanExecuteGenerateMatchingCommandHandling(bool startTimeHourError, bool startTimeMinuteError, bool endTimeHourError, bool endTimeMinuteError, bool expectation)
        {
            #region Arange

            this.mv.Mentees = this.GenerateMentees();
            this.mv.Mentors = this.GenerateMentors();
            this.mv.StartTimeHoursHasErrors = startTimeHourError;
            this.mv.StartTimeMinutesHasErrors = startTimeMinuteError;
            this.mv.EndTimeHoursHasErrors= endTimeHourError;
            this.mv.EndTimeMinutesHasErrors = endTimeMinuteError;

            #endregion

            #region Act

            var canMatch = this.mv.CanExecuteGenerateMatchingCommandHandling();

            #endregion

            #region Assert

            Assert.Equal(canMatch, expectation);

            #endregion
        }

        [Fact]
        public void Test_LoadSavedDataReturns_WhenFileNotFound()
        {
            #region Arange

            this.mv.Mentees = new ObservableCollection<Mentee>();
            this.mv.Mentors = new ObservableCollection<Mentor>();
            this.mv.StartTimeHours = "TestZweck";
            this.mv.StartTimeMinutes = "TestZweck";
            this.mv.EndTimeHours = "TestZweck";
            this.mv.EndTimeMinutes = "TestZweck";
            this.mv.Headline = "TestZweck";

            #endregion

            #region Act

            this.mv.LoadSavedData(@"..\..\..\..\SavedData\noFileFound.json");

            #endregion

            #region Assert

            Assert.Empty(this.mv.Mentees);
            Assert.Empty(this.mv.Mentors);
            Assert.Equal("TestZweck", this.mv.StartTimeHours);
            Assert.Equal("TestZweck", this.mv.StartTimeMinutes);
            Assert.Equal("TestZweck", this.mv.EndTimeHours);
            Assert.Equal("TestZweck", this.mv.EndTimeMinutes);
            Assert.Equal("TestZweck", this.mv.Headline);

            #endregion
        }

        [Fact]
        public void Test_LoadSavedData_WhenFileFound()
        {
            #region Arange

            this.mv.Mentees = new ObservableCollection<Mentee>();
            this.mv.Mentors = new ObservableCollection<Mentor>();
            this.mv.Mentors = new ObservableCollection<Mentor>();
            this.mv.StartTimeHours = "alt";
            this.mv.StartTimeMinutes = "alt";
            this.mv.EndTimeHours = "alt";
            this.mv.EndTimeMinutes = "alt";
            this.mv.Headline= "alt";

            #endregion

            #region Act

            this.mv.LoadSavedData(path: @"..\..\..\..\MentorSpeedDatingApp\SavedData\test_data.json");

            #endregion

            #region Assert

            Assert.Equal("neu", this.mv.StartTimeHours);
            Assert.Equal("neu", this.mv.StartTimeMinutes);
            Assert.Equal("neu", this.mv.EndTimeHours);
            Assert.Equal("neu", this.mv.EndTimeMinutes);
            Assert.Contains(this.mv.Mentees, mentee => mentee.Name=="Wilde");
            Assert.Contains(this.mv.Mentors, mentor => mentor.Name == "Morgan");

            #endregion
        }

        [Fact]
        public void Test_OnClosingDetectUnsavedChanges()
        {
            #region Arange

            this.mv.Mentees = this.GenerateMentees();
            this.mv.Mentors = this.GenerateMentors();
            this.mv.StartTimeHours = "alt";
            this.mv.StartTimeMinutes = "alt";
            this.mv.EndTimeHours = "alt";
            this.mv.EndTimeMinutes = "alt";
            this.mv.Headline = "alt";

            #endregion

            #region Act

            var hasChanged = this.mv.OnClosingDetectUnsavedChanges(@"..\..\..\..\MentorSpeedDatingApp\SavedData\test_data.json");

            #endregion

            #region Assert

            Assert.True(hasChanged);

            #endregion
        }
    }
}