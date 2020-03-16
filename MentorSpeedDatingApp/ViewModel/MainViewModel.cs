using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.RightsManagement;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MentorSpeedDatingApp.Models;
using Newtonsoft.Json;

namespace MentorSpeedDatingApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        #endregion

        #region Lists

        public ObservableCollection<Mentee> Mentees { get; set; }
        public ObservableCollection<Mentor> Mentors { get; set; }

        #endregion

        #region RelayCommands

        public RelayCommand OnLoadCommand { get; set; }
        public RelayCommand OnCloseCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand GenerateMatchingCommand { get; set; }

        #endregion

        public MainViewModel()
        {
            this.Mentees = new ObservableCollection<Mentee>();
            this.Mentors = new ObservableCollection<Mentor>();

            this.OnLoadCommand = new RelayCommand(this.OnLoadCommandHandling);
            this.OnCloseCommand = new RelayCommand(this.OnCloseCommandHandling);
            this.SaveCommand = new RelayCommand(this.SaveCommandHandling);
            this.GenerateMatchingCommand = new RelayCommand(this.GenerateMatchingCommandHandling);

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

        private void GenerateMatchingCommandHandling()
        {
            throw new NotImplementedException();
        }

        private void SaveCommandHandling()
        {
            var jsonMentors = JsonConvert.SerializeObject(this.Mentors, Formatting.Indented);
            var jsonMentees = JsonConvert.SerializeObject(this.Mentees, Formatting.Indented);

            File.WriteAllText( @"..\..\..\..\SavedData\mentors.json", jsonMentors);
            File.WriteAllText(@"..\..\..\..\SavedData\mentees.json", jsonMentees);
        }

        private void OnCloseCommandHandling()
        {
            throw new NotImplementedException();
        }

        private void OnLoadCommandHandling()
        {
            var jsonMentorsData = File.ReadAllText(@"..\..\..\..\SavedData\mentors.json");
            this.Mentors = JsonConvert.DeserializeObject<ObservableCollection<Mentor>>(jsonMentorsData);

            var jsonMenteesData = File.ReadAllText(@"..\..\..\..\SavedData\mentees.json");
            this.Mentees = JsonConvert.DeserializeObject<ObservableCollection<Mentee>>(jsonMenteesData);
        }
    }
}