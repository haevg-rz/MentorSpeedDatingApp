using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.RightsManagement;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MentorSpeedDatingApp.Models;

namespace MentorSpeedDatingApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
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
                    new Mentee {Vorname = "Max", Name = "Mustermann", Titel = "Dr."},
                    new Mentee {Vorname = "Frerik", Name = "Ebert", Titel = "Prof."},
                    new Mentee {Vorname = "Teo", Name = "Limbert", Titel = "Dr. med."},
                    new Mentee {Vorname = "Dereck", Name = "Watson", Titel = "Prof. Dr."},
                    new Mentee {Vorname = "Jack", Name = "Sparrow", Titel = ""}
                };
                this.Mentors = new ObservableCollection<Mentor>
                {
                    new Mentor {Vorname = "Benedickt", Name = "Cumbersnatch", Titel = "Dr."},
                    new Mentor {Vorname = "Brad", Name = "Pitt", Titel = ""},
                    new Mentor {Vorname = "Leonardo", Name = "DaVinci", Titel = ""},
                    new Mentor {Vorname = "Vincent", Name = "Van Gohg", Titel = ""},
                    new Mentor {Vorname = "Rosetta", Name = "Brown", Titel = ""}
                };
            }
        }

        private void GenerateMatchingCommandHandling()
        {
            throw new NotImplementedException();
        }

        private void SaveCommandHandling()
        {
            throw new NotImplementedException();
        }

        private void OnCloseCommandHandling()
        {
            throw new NotImplementedException();
        }

        private void OnLoadCommandHandling()
        {
            throw new NotImplementedException();
        }
    }
}