using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MentorSpeedDatingApp.ExtraFunctions;
using MentorSpeedDatingApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Xps.Packaging;
using System.Xml;
using GalaSoft.MvvmLight.Ioc;
using MentorSpeedDatingApp.WindowManagement;
using Formatting = Newtonsoft.Json.Formatting;

[assembly: InternalsVisibleTo("MentorSpeedDatingApp.MentorSpeedDatingAppTest")]


namespace MentorSpeedDatingApp.ViewModel
{
    [DataContract]
    public class MainViewModel : ViewModelBase
    {
        #region ClassMembers

        #region Properties

        private string headline;
        [DataMember]
        public string Headline
        {
            get => this.headline;
            set => base.Set(ref this.headline, value);
        }

        private DateTime date;
        [DataMember]
        public DateTime Date
        {
            get => this.date;
            set => base.Set(ref this.date, value);
        }

        #region Time Properties

        private string startTimeHours;
        [DataMember]
        public string StartTimeHours
        {
            get => this.startTimeHours;
            set => base.Set(ref this.startTimeHours, value);
        }

        private bool startTimeHoursHasErrors;
        public bool StartTimeHoursHasErrors
        {
            get => this.startTimeHoursHasErrors;
            set => base.Set(ref this.startTimeHoursHasErrors, value);
        }

        private string startTimeMinutes;
        [DataMember]
        public string StartTimeMinutes
        {
            get => this.startTimeMinutes;
            set => base.Set(ref this.startTimeMinutes, value);
        }

        private bool startTimeMinutesHasErrors;
        public bool StartTimeMinutessHasErrors
        {
            get => this.startTimeMinutesHasErrors;
            set => base.Set(ref this.startTimeMinutesHasErrors, value);
        }

        private string endTimeHours;
        [DataMember]
        public string EndTimeHours
        {
            get => this.endTimeHours;
            set => base.Set(ref this.endTimeHours, value);
        }

        private bool endTimeHoursHasErrors;
        public bool EndTimeHoursHasErrors
        {
            get => this.endTimeHoursHasErrors;
            set => base.Set(ref this.endTimeHoursHasErrors, value);
        }

        private string endTimeMinutes;
        [DataMember]
        public string EndTimeMinutes
        {
            get => this.endTimeMinutes;
            set => base.Set(ref this.endTimeMinutes, value);
        }

        private bool endTimeMinutesHasErrors;
        public bool EndTimeMinutesHasErrors
        {
            get => this.endTimeMinutesHasErrors;
            set => base.Set(ref this.endTimeMinutesHasErrors, value);
        }

        private List<DateTime> timeSlots;
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
        public RelayCommand PrintCommand { get; set; }
        public RelayCommand ShowInfoCommand { get; set; }

        #endregion

        #endregion

        public MainViewModel()
        {
            this.Mentees = new ObservableCollection<Mentee>();
            this.Mentors = new ObservableCollection<Mentor>();

            this.SaveCommand = new RelayCommand(this.SaveCommandHandling);
            this.GenerateMatchingCommand = new RelayCommand(this.GenerateMatchingCommandHandling,
                this.CanExecuteGenerateMatchingCommandHandling);
            this.DeleteMentorsCommand = new RelayCommand(this.DeleteMentorsCommandHandling);
            this.DeleteMenteesCommand = new RelayCommand(this.DeleteMenteesCommandHandling);
            this.DeleteAllDataCommand = new RelayCommand(this.DeleteAllDataCommandHandling);
            this.PrintCommand = new RelayCommand(this.PrintCommandHandling);
            this.ShowInfoCommand = new RelayCommand(this.ShowInfoCommandHandling);

            this.OnLoadCommandHandling();

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
            MainViewModel mvm = this;
            WindowManager.ShowMatchingWindow(mvm);
        }

        private bool CanExecuteGenerateMatchingCommandHandling()
        {
            return this.Mentors.Any() && this.Mentees.Any() && !this.ValidationRulesHasError;
        }

        private void SaveCommandHandling()
        {
            var saveConfirmation = MessageBox.Show("Speichern?", button: MessageBoxButton.OKCancel, caption:"Speichern");
            if (saveConfirmation == MessageBoxResult.Yes)
            {
                var jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(@"..\..\..\..\SavedData\data.json", jsonData);
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
                Date = DateTime.Now,
                StartTimeHours = "",
                StartTimeMinutes = "",
                EndTimeHours = "",
                EndTimeMinutes = "",
                Mentees = new List<Mentee>(),
                Mentors = new List<Mentor>()
            };
            var jsonData = File.ReadAllText(@"..\..\..\..\SavedData\data.json");
            var deserializedJson = JsonConvert.DeserializeAnonymousType(jsonData, definition);

            this.Headline = deserializedJson.HeadLine;
            this.Date = deserializedJson.Date;
            this.StartTimeHours = deserializedJson.StartTimeHours;
            this.StartTimeMinutes = deserializedJson.StartTimeMinutes;
            this.EndTimeHours = deserializedJson.EndTimeHours;
            this.EndTimeMinutes = deserializedJson.EndTimeMinutes;
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

        private void PrintCommandHandling()
        {
            PrintDialog pDialog = new PrintDialog();
            pDialog.PageRangeSelection = PageRangeSelection.AllPages;
            pDialog.UserPageRangeEnabled = true;

            //DruckerName ist unzulässig Exception
            bool? print = pDialog.ShowDialog();
            if (print == true)
            {
                XpsDocument xps = new XpsDocument("C:\\FixedDocumentSequence.xps", FileAccess.ReadWrite);
                FixedDocumentSequence fixedDocSeq = xps.GetFixedDocumentSequence();
                pDialog.PrintDocument(fixedDocSeq.DocumentPaginator, "Test print job");
            }
        }

        private void ShowInfoCommandHandling()
        {
            MessageBox.Show(
                messageBoxText:
                "Version 0.1.0 \nDiese App wurde vom HÄVGRZ-Alphateam entwickelt.\nhttps://github.com/haevg-rz/MentorSpeedDatingApp", caption:"App-Informationen");
        }

        #endregion

        #region Helpermethods

        public static MessageBoxResult OnCloseCommand()
        {
            var userDecision = new MessageBoxResult();

            if (!File.Exists(@"..\..\..\..\SavedData\data.json"))
            {
                return userDecision;
            }

            if (SimpleIoc.Default.GetInstance<MainViewModel>().OnClosingDetectUnsavedChanges())
            {
                userDecision = MessageBox.Show("Ungespeicherte Änderungen sind vorhanden!\n" +
                                               "Drücken Sie \"OK\" zum verwerfen, oder\n" +
                                               "\"Abbrechen\", um in die Anwendung zurückzukehren.",
                    "Warnung", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }

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
            var jsonData = File.ReadAllText(@"..\..\..\..\SavedData\data.json");
            var deserializedJson = JsonConvert.DeserializeAnonymousType(jsonData, definition);

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