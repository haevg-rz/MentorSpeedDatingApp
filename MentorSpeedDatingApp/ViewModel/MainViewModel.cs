using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.RightsManagement;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MentorSpeedDatingApp.ExtraFunctions;
using MentorSpeedDatingApp.Models;
using Newtonsoft.Json;

namespace MentorSpeedDatingApp.ViewModel
{
    [DataContract]
    public class MainViewModel : ViewModelBase
    {
        #region ClassMembers

        #region Fields

        #endregion

        #region Properties

        private string headline;

        [DataMember]
        public string Headline
        {
            get => this.headline;
            set => base.Set(ref this.headline, value);
        }

        private DateTime date;

        public DateTime Date
        {
            get => this.date;
            set => base.Set(ref this.date, value);
        }

        private string startTimeHours;
        [DataMember]
        public string StartTimeHours
        {
            get => this.startTimeHours;
            set => base.Set(ref this.startTimeHours, value);
        }

        private string startTimeMinutes;

        public string StartTimeMinutes
        {
            get => this.startTimeMinutes;
            set => base.Set(ref this.startTimeMinutes, value);
        }

        private string endTimeHours;
        [DataMember]
        public string EndTimeHours
        {
            get => this.endTimeHours;
            set => base.Set(ref this.endTimeHours, value);
        }

        private string endTimeMinutes;
        public string EndTimeMinutes
        {
            get => this.endTimeMinutes;
            set => base.Set(ref this.endTimeMinutes, value);
        }

        private string startTime;
        public string StartTime
        {
            get => this.startTime;
            set
            {
                value = this.BuildTimes(this.StartTimeHours, this.StartTimeMinutes);
                base.Set(ref this.startTime, value);
            } 
        }

        private string endTime;
        public string EndTime
        {
            get => this.endTime;
            set
            {
                value = this.BuildTimes(this.EndTimeHours, this.EndTimeMinutes);
                base.Set(ref this.endTime, value);
            } 
        }

        #endregion

        #region Lists

        [DataMember] public ObservableCollection<Mentee> Mentees { get; set; }
        [DataMember] public ObservableCollection<Mentor> Mentors { get; set; }

        #endregion

        #region RelayCommands

        public RelayCommand OnLoadCommand { get; set; }
        public RelayCommand OnCloseCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand GenerateMatchingCommand { get; set; }
        public RelayCommand DeleteMentorsCommand { get; set; }
        public RelayCommand DeleteMenteesCommand { get; set; }

        #endregion

        #endregion

        public MainViewModel()
        {
            this.StartTimeHours = "00";
            this.StartTimeMinutes = "00";
            this.EndTimeHours = "00";
            this.EndTimeMinutes = "00";
            this.Date = DateTime.Now;

            this.Mentees = new ObservableCollection<Mentee>();
            this.Mentors = new ObservableCollection<Mentor>();

            this.OnLoadCommand = new RelayCommand(this.OnLoadCommandHandling);
            this.OnCloseCommand = new RelayCommand(this.OnCloseCommandHandling);
            this.SaveCommand = new RelayCommand(this.SaveCommandHandling);
            this.GenerateMatchingCommand = new RelayCommand(this.GenerateMatchingCommandHandling, this.CanExecuteGenerateMatchingCommandHandling);
            this.DeleteMentorsCommand = new RelayCommand(this.DeleteMentorsCommandHandling);
            this.DeleteMenteesCommand = new RelayCommand(this.DeleteMenteesCommandHandling);

            this.OnLoadCommandHandling();

            if (base.IsInDesignMode)
            {
                this.Mentees = new ObservableCollection<Mentee>
                {
                    new Mentee {Vorname = "Scarlett", Name = "Johansson", Titel = "Dr."},
                    new Mentee {Vorname = "Jennifer", Name = "Lawrence", Titel = "Prof."},
                    new Mentee {Vorname = "Julia", Name = "Roberts", Titel = "Dr. med."},
                    new Mentee {Vorname = "Jennifer", Name = "Aniston", Titel = "Prof. Dr."},
                    new Mentee {Vorname = "Sandra", Name = "Bullock", Titel = ""}
                };
                this.Mentors = new ObservableCollection<Mentor>
                {
                    new Mentor {Vorname = "Natalie", Name = "Portman", Titel = "Dr."},
                    new Mentor {Vorname = "Meryl", Name = "Streep", Titel = ""},
                    new Mentor {Vorname = "Angelina", Name = "Jolie", Titel = "Dr. Dr."},
                    new Mentor {Vorname = "Charlize", Name = "Theron", Titel = ""},
                    new Mentor {Vorname = "Emma", Name = "Watson", Titel = "Prof."}
                };
            }
        }

        #region CommandHandlings

        private void DeleteMenteesCommandHandling()
        {
            throw new NotImplementedException();
        }

        private void DeleteMentorsCommandHandling()
        {
            throw new NotImplementedException();
        }

        private void GenerateMatchingCommandHandling()
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteGenerateMatchingCommandHandling()
        {
            return true;
        }

        private void SaveCommandHandling()
        {
            Mentors.Add(new Mentor(){Titel = "nawd",Name = "nad",Vorname = "add"});
            Mentees.Add(new Mentee() { Titel = "nawd", Name = "nad", Vorname = "add" });
            return;
            var jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);

            File.WriteAllText(@"..\..\..\..\SavedData\data.json", jsonData);
        }

        private void OnCloseCommandHandling()
        {
            if (!File.Exists(@"..\..\..\..\SavedData\data.json"))
            {
                return;
            }

            var definition = new
            {
                HeadLine = "",
                StartTime = "",
                EndTime = "",
                Mentees = new ObservableCollection<Mentee>(),
                Mentors = new ObservableCollection<Mentor>()
            };
            var jsonData = File.ReadAllText(@"..\..\..\..\SavedData\data.json");
            var deserializedJson = JsonConvert.DeserializeAnonymousType(jsonData, definition);

            if (!this.Mentors.CompareCollectionsOnEqualContent(deserializedJson.Mentors,
                (mVM, mSerialized) => mVM.Name == mSerialized.Name && mVM.Vorname == mSerialized.Vorname &&
                                      mVM.Titel == mSerialized.Titel))
            {
                MessageBox.Show("Ungespeicherte Änderungen sind vorhanden!");
            }
        }

        private void OnLoadCommandHandling()
        {
            if (!File.Exists(@"..\..\..\..\SavedData\data.json"))
            {
                return;
            }

            var definition = new
            {
                HeadLine = "",
                StartTime = "",
                EndTime = "",
                Mentees = new ObservableCollection<Mentee>(),
                Mentors = new ObservableCollection<Mentor>()
            };
            var jsonData = File.ReadAllText(@"..\..\..\..\SavedData\data.json");
            var deserializedJson = JsonConvert.DeserializeAnonymousType(jsonData, definition);

            this.Headline = deserializedJson.HeadLine;
            this.StartTime = deserializedJson.StartTime;
            this.EndTime = deserializedJson.EndTime;
            this.Mentors.Clear();
            this.Mentees.Clear();
            foreach (var mentor in deserializedJson.Mentors)
            {
                this.Mentors.Add(mentor);
            }
            foreach (var mentee in deserializedJson.Mentees)
            {
                this.Mentees.Add(mentee);
            }
        }

        #endregion

        #region Helpermethods

        private string BuildTimes(string hours, string minutes)
        {
            var sb = new StringBuilder();
            sb.Append(hours);
            sb.Append(":");
            sb.Append(minutes);
            return sb.ToString();
        }

        #endregion

    }
}