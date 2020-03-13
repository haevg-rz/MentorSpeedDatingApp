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

        #endregion


        public MainViewModel()
        {

        }
    }
}