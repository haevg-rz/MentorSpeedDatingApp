﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using MentorSpeedDatingApp.ExtraFunctions;
using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.WindowManagement;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Windows;
using Formatting = Newtonsoft.Json.Formatting;

[assembly: InternalsVisibleTo("MentorSpeedDatingApp.MentorSpeedDatingAppTest")]

namespace MentorSpeedDatingApp.ViewModel
{
    [DataContract]
    public class MainViewModel : ViewModelBase
    {
        #region ClassMembers

        #region Properties

        public Config AppSaveConfig { get; set; } = new Config();

        private string headline = "";

        [DataMember]
        public string Headline
        {
            get => this.headline;
            set => base.Set(ref this.headline, value);
        }

        private DateTime date = DateTime.Now;

        [DataMember]
        public DateTime Date
        {
            get => this.date;
            set => base.Set(ref this.date, value);
        }

        #region Time Properties

        private string startTimeHours = "";

        [DataMember]
        public string StartTimeHours
        {
            get => this.startTimeHours;
            set => base.Set(ref this.startTimeHours, value);
        }

        private bool startTimeHoursHasErrors = false;

        public bool StartTimeHoursHasErrors
        {
            get => this.startTimeHoursHasErrors;
            set => base.Set(ref this.startTimeHoursHasErrors, value);
        }

        private string startTimeMinutes = "";

        [DataMember]
        public string StartTimeMinutes
        {
            get => this.startTimeMinutes;
            set => base.Set(ref this.startTimeMinutes, value);
        }

        private bool startTimeMinutesHasErrors = false;

        public bool StartTimeMinutessHasErrors
        {
            get => this.startTimeMinutesHasErrors;
            set => base.Set(ref this.startTimeMinutesHasErrors, value);
        }

        private string endTimeHours = "";

        [DataMember]
        public string EndTimeHours
        {
            get => this.endTimeHours;
            set => base.Set(ref this.endTimeHours, value);
        }

        private bool endTimeHoursHasErrors = false;

        public bool EndTimeHoursHasErrors
        {
            get => this.endTimeHoursHasErrors;
            set => base.Set(ref this.endTimeHoursHasErrors, value);
        }

        private string endTimeMinutes = "";

        [DataMember]
        public string EndTimeMinutes
        {
            get => this.endTimeMinutes;
            set => base.Set(ref this.endTimeMinutes, value);
        }

        private bool endTimeMinutesHasErrors = false;

        public bool EndTimeMinutesHasErrors
        {
            get => this.endTimeMinutesHasErrors;
            set => base.Set(ref this.endTimeMinutesHasErrors, value);
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        private List<DateTime> timeSlots = new List<DateTime>();

        public List<DateTime> TimeSlots
        {
            get => this.timeSlots;
            set => base.Set(ref this.timeSlots, value);
        }

        #endregion

        public bool ValidationRulesHasError => this.startTimeHoursHasErrors || this.startTimeMinutesHasErrors ||
                                               this.endTimeHoursHasErrors || this.endTimeMinutesHasErrors;

        #endregion

        #region Lists

        [DataMember] public ObservableCollection<Mentee> Mentees { get; set; }
        [DataMember] public ObservableCollection<Mentor> Mentors { get; set; }

        #endregion

        #region RelayCommands

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand GenerateMatchingCommand { get; set; }

        public RelayCommand DeleteMentorsCommand { get; set; }
        public RelayCommand DeleteMenteesCommand { get; set; }
        public RelayCommand DeleteAllDataCommand { get; set; }
        public RelayCommand ShowInfoCommand { get; set; }

        public RelayCommand OnLoadedCommand { get; set; }

        #endregion

        #endregion

        public MainViewModel()
        {
            this.Mentees = new ObservableCollection<Mentee>();
            this.Mentors = new ObservableCollection<Mentor>();
            this.StartTime = new DateTime();
            this.EndTime = new DateTime();

            this.SaveCommand = new RelayCommand(this.SaveCommandHandling);
            this.GenerateMatchingCommand = new RelayCommand(this.GenerateMatchingCommandHandling,
                this.CanExecuteGenerateMatchingCommandHandling);
            this.DeleteMentorsCommand = new RelayCommand(this.DeleteMentorsCommandHandling);
            this.DeleteMenteesCommand = new RelayCommand(this.DeleteMenteesCommandHandling);
            this.DeleteAllDataCommand = new RelayCommand(this.DeleteAllDataCommandHandling);
            this.ShowInfoCommand = new RelayCommand(this.ShowInfoCommandHandling);

            this.OnLoadedCommand = new RelayCommand(this.OnLoadedCommandHandling);

            if (base.IsInDesignMode || this.IsInDesignMode)
            {
                this.StartTimeHours = "12";
                this.StartTimeMinutes = "00";
                this.EndTimeHours = "18";
                this.EndTimeMinutes = "00";
                this.Date = DateTime.Now;

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

        private void DeleteAllDataCommandHandling()
        {
            this.Headline = "";
            this.StartTimeHours = "";
            this.StartTimeMinutes = "";
            this.EndTimeHours = "";
            this.EndTimeMinutes = "";
            this.Date = DateTime.Now;
            this.Mentors.Clear();
            this.Mentees.Clear();
        }

        private void DeleteMenteesCommandHandling()
        {
            this.Mentees.Clear();
        }

        private void DeleteMentorsCommandHandling()
        {
            this.Mentors.Clear();
        }

        private void GenerateMatchingCommandHandling()
        {
            if (String.IsNullOrEmpty(this.StartTimeHours) || String.IsNullOrEmpty(this.StartTimeMinutes))
            {
                this.StartTime = new DateTime(this.Date.Year, this.Date.Month, this.Date.Day, 7,
                    0, 00);
            }
            else
            {
                this.StartTime = new DateTime(this.Date.Year, this.Date.Month, this.Date.Day,
                    Convert.ToInt32(this.StartTimeHours),
                    Convert.ToInt32(this.StartTimeMinutes), 00);
            }

            if (String.IsNullOrEmpty(this.EndTimeHours) || String.IsNullOrEmpty(this.EndTimeMinutes))
            {
                this.EndTime = new DateTime(this.Date.Year, this.Date.Month, this.Date.Day, 13,
                    0, 00);
            }
            else
            {
                this.EndTime = new DateTime(this.Date.Year, this.Date.Month, this.Date.Day,
                    Convert.ToInt32(this.EndTimeHours),
                    Convert.ToInt32(this.EndTimeMinutes), 00);
            }

            MainViewModel mvm = this;
            WindowManager.ShowMatchingWindow(mvm);
        }

        private bool CanExecuteGenerateMatchingCommandHandling()
        {
            return this.Mentors.Any() && this.Mentees.Any() && !this.ValidationRulesHasError;
        }

        private void SaveCommandHandling()
        {
            var combinedPath = this.AppSaveConfig.AppSaveFileFolder;

            var sfd = new SaveFileDialog
            {
                InitialDirectory = this.AppSaveConfig.AppSaveFileFolder,
                FileName = this.AppSaveConfig.AppSaveFileName,
                Filter = "JSON Files(*.json) | *.json|All Files(*.*) | *.*",
                DefaultExt = "JSON Files (*.json) | .json"
            };

            if (sfd.ShowDialog() != true)
                return;

            var jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
            this.AppSaveConfig.AppSaveFileName = sfd.FileName;
            this.AppSaveConfig.AppSaveFileFolder = Path.GetDirectoryName(sfd.FileName);
            File.WriteAllText( Path.Combine(this.AppSaveConfig.AppSaveFileFolder, this.AppSaveConfig.AppSaveFileName), jsonData);
        }

        private void OnLoadedCommandHandling()
        {
            #region Config/Default erstellen

            if (!Directory.Exists(this.AppSaveConfig.AppDefaultFolder))
            {
                Directory.CreateDirectory(this.AppSaveConfig.AppDefaultFolder);
            }

            if (!File.Exists(this.AppSaveConfig.AppConfigPath))
            {
                File.Create(this.AppSaveConfig.AppConfigPath).Close();
                var defaultConfig = new
                    { Folder = this.AppSaveConfig.AppDefaultFolder, FileName = this.AppSaveConfig.AppDefaultFileName};
                var jsonConfig = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
                File.WriteAllText(this.AppSaveConfig.AppConfigPath, jsonConfig);
            }

            var fileContent = File.ReadAllText(this.AppSaveConfig.AppConfigPath);
            if (String.IsNullOrEmpty(fileContent))
            {
                var defaultConfig = new
                    { Folder = this.AppSaveConfig.AppSaveFileFolder, FileName = this.AppSaveConfig.AppSaveFileName };
                var jsonConfig = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
                File.WriteAllText(this.AppSaveConfig.AppConfigPath, jsonConfig);
            }
            
            #endregion

            #region Config laden

            var configDefinition= new {Folder = "", FileName = ""};

            var savedConfig = File.ReadAllText(this.AppSaveConfig.AppConfigPath);
            var savedConfigDeserialzed = JsonConvert.DeserializeAnonymousType(savedConfig, configDefinition);

            if (savedConfigDeserialzed==null)
                return;
            this.AppSaveConfig.AppSaveFileFolder = savedConfigDeserialzed.Folder;
            this.AppSaveConfig.AppSaveFileName = savedConfigDeserialzed.FileName;
            if (!File.Exists(this.AppSaveConfig.CombineAppPaths()))
            {
                this.AppSaveConfig.ResetToDefault();
            }

            #endregion

            #region Speicherdaten laden

            var saveDataDefinition = new
            {
                HeadLine = "",
                Date = DateTime.Now,
                StartTimeHours = "",
                StartTimeMinutes = "",
                EndTimeHours = "",
                EndTimeMinutes = "",
                Mentees = new List<Mentee>(),
                Mentors = new List<Mentor>()
            };

            if (!File.Exists(this.AppSaveConfig.CombineAppPaths()))
                return;
            var jsonData = File.ReadAllText(Path.Combine(this.AppSaveConfig.AppSaveFileFolder, this.AppSaveConfig.AppSaveFileName));
            var deserializedJson = JsonConvert.DeserializeAnonymousType(jsonData, saveDataDefinition);

            if (deserializedJson == null)
                return;

            this.Headline = deserializedJson.HeadLine;
            this.Date = deserializedJson.Date;
            this.StartTimeHours = deserializedJson.StartTimeHours;
            this.StartTimeMinutes = deserializedJson.StartTimeMinutes;
            this.EndTimeHours = deserializedJson.EndTimeHours;
            this.EndTimeMinutes = deserializedJson.EndTimeMinutes;

            foreach (var mentor in deserializedJson.Mentors)
            {
                this.Mentors.Add(mentor);
            }

            foreach (var mentee in deserializedJson.Mentees)
            {
                this.Mentees.Add(mentee);
            }
        }

        private void ShowInfoCommandHandling()
        {
            MessageBox.Show(
                messageBoxText:
                "Version 0.1.0 \nDiese App wurde vom HÄVGRZ-Alphateam entwickelt.\nhttps://github.com/haevg-rz/MentorSpeedDatingApp",
                caption: "App-Informationen");
        }

        #endregion

        #region Helpermethods

        public MessageBoxResult OnCloseCommand()
        {
            var userDecision = new MessageBoxResult();

            if (this.OnClosingDetectUnsavedChanges())
            {
                userDecision = MessageBox.Show("Ungespeicherte Änderungen sind vorhanden!\n" +
                                               "Drücken Sie \"OK\" zum verwerfen, oder\n" +
                                               "\"Abbrechen\", um in die Anwendung zurückzukehren.",
                    "Warnung", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }

            #region Config überschreiben

            var newConfig = new
                { Folder = this.AppSaveConfig.AppSaveFileFolder, FileName = this.AppSaveConfig.AppSaveFileName };
            var jsonConfig = JsonConvert.SerializeObject(newConfig, Formatting.Indented);
            File.WriteAllText(this.AppSaveConfig.AppConfigPath, jsonConfig);

            #endregion

            return userDecision;
        }

        private bool OnClosingDetectUnsavedChanges()
        {
            var definition = new
            {
                HeadLine = "",
                Date = DateTime.Now,
                StartTimeHours = "",
                StartTimeMinutes = "",
                EndTimeHours = "",
                EndTimeMinutes = "",
                Mentees = new List<Mentee>(),
                Mentors = new List<Mentor>()
            };

            var jsonData = File.ReadAllText(Path.Combine(this.AppSaveConfig.AppSaveFileFolder, this.AppSaveConfig.AppSaveFileName));
            var deserializedJson = JsonConvert.DeserializeAnonymousType(jsonData, definition);
            if (deserializedJson == null && jsonData == "")
                return false;

            return !this.Mentors.CompareCollectionsOnEqualContent(deserializedJson.Mentors,
                       (mVM, mSerialized) => mVM.Name == mSerialized.Name
                                             && mVM.Vorname == mSerialized.Vorname
                                             && mVM.Titel == mSerialized.Titel)
                   || !this.Mentees.CompareCollectionsOnEqualContent(deserializedJson.Mentees,
                       (mVM, mSerialized) => mVM.Name == mSerialized.Name
                                             && mVM.Vorname == mSerialized.Vorname
                                             && mVM.Titel == mSerialized.Titel)
                   || this.Headline != deserializedJson.HeadLine
                   || this.Date != deserializedJson.Date
                   || this.StartTimeHours != deserializedJson.StartTimeHours
                   || this.StartTimeMinutes != deserializedJson.StartTimeMinutes
                   || this.EndTimeHours != deserializedJson.EndTimeHours
                   || this.EndTimeMinutes != deserializedJson.EndTimeMinutes;
        }

        #endregion
    }
}