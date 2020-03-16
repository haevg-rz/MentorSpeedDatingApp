using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.RightsManagement;
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

        private string headLine;

        [DataMember]
        public string HeadLine
        {
            get => this.headLine;
            set => base.Set(ref this.headLine, value);
        }

        private DateTime startTime;

        [DataMember]
        public DateTime StartTime
        {
            get => this.startTime;
            set => base.Set(ref this.startTime, value);
        }

        private DateTime endTime;

        [DataMember]
        public DateTime EndTime
        {
            get => this.endTime;
            set => base.Set(ref this.endTime, value);
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
            this.Date = DateTime.Now;

            this.Mentees = new ObservableCollection<Mentee>();
            this.Mentors = new ObservableCollection<Mentor>();

            this.OnLoadCommand = new RelayCommand(this.OnLoadCommandHandling);
            this.OnCloseCommand = new RelayCommand(this.OnCloseCommandHandling);
            this.SaveCommand = new RelayCommand(this.SaveCommandHandling);
            this.GenerateMatchingCommand = new RelayCommand(this.GenerateMatchingCommandHandling);
            this.DeleteMentorsCommand = new RelayCommand(this.DeleteMentorsCommandHandling);
            this.DeleteMenteesCommand = new RelayCommand(this.DeleteMenteesCommandHandling);

            this.OnLoadCommandHandling();

            if (base.IsInDesignMode)
            {
                this.Date = DateTime.Today;
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

        private void SaveCommandHandling()
        {
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
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
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
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Mentees = new ObservableCollection<Mentee>(),
                Mentors = new ObservableCollection<Mentor>()
            };
            var jsonData = File.ReadAllText(@"..\..\..\..\SavedData\data.json");
            var deserializedJson = JsonConvert.DeserializeAnonymousType(jsonData, definition);

            this.HeadLine = deserializedJson.HeadLine;
            this.StartTime = deserializedJson.StartTime;
            this.EndTime = deserializedJson.EndTime;
            this.Mentors = deserializedJson.Mentors;
            this.Mentees = deserializedJson.Mentees;
        }

        #endregion
    }
}