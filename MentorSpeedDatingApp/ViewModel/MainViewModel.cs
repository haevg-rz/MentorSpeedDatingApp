using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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

        public String TableToolTip { get; set; } = "Neuen Eintrag mit Rechtsklick hinzufügen...";

        public Config AppSaveConfig { get; set; } = new Config();

        private string headline = "";

        private int maxDruckSpalten = 8;

        [DataMember]
        public string Headline
        {
            get => this.headline;
            set => base.Set(ref this.headline, value);
        }

        public int MaxDruckSpalten
        {
            get => this.maxDruckSpalten;
            set => base.Set(ref this.maxDruckSpalten, value);
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


        public List<string> AllowedHourValues { get; set; } = new List<string>
        {
            "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17",
            "18", "19", "20", "21", "22", "23"
        };

        public List<string> AllowedMinuteValues { get; set; } = new List<string>
        {
            "00", "05", "10", "15", "20", "25", "30", "35", "40", "45", "50", "55"
        };

        #endregion

        public bool ValidationRulesHasError => this.startTimeHoursHasErrors || this.startTimeMinutesHasErrors ||
                                               this.endTimeHoursHasErrors || this.endTimeMinutesHasErrors;

        public Mentor NoGoMentor { get; set; }
        public Mentee NoGoMentee { get; set; }

        #endregion

        #region Lists

        [DataMember] public ObservableCollection<Mentee> Mentees { get; set; }
        [DataMember] public ObservableCollection<Mentor> Mentors { get; set; }
        [DataMember] public ObservableCollection<(Mentor, Mentee)> NoGoDates { get; set; }

        #endregion

        #region RelayCommands

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand GenerateMatchingCommand { get; set; }
        public RelayCommand AddNoGoDateCommand { get; set; }

        public RelayCommand DeleteMentorsCommand { get; set; }
        public RelayCommand DeleteMenteesCommand { get; set; }
        public RelayCommand DeleteAllDataCommand { get; set; }
        public RelayCommand ShowInfoCommand { get; set; }
        public RelayCommand ShowNoGoDatesCommand { get; set; }

        public RelayCommand OnLoadedCommand { get; set; }

        public RelayCommand AddNewMentorCommand { get; set; }
        public RelayCommand AddNewMenteeCommand { get; set; }
        public RelayCommand GenerateMatchingWithNoGoDatesCommand { get; set; }

        #endregion

        #endregion

        public MainViewModel()
        {
            this.Mentees = new ObservableCollection<Mentee>();
            this.Mentors = new ObservableCollection<Mentor>();
            this.NoGoDates = new ObservableCollection<(Mentor, Mentee)>();
            this.StartTime = new DateTime();
            this.EndTime = new DateTime();

            this.SaveCommand = new RelayCommand(this.SaveCommandHandling);
            this.GenerateMatchingCommand = new RelayCommand(this.GenerateMatchingCommandHandling,
                this.CanExecuteGenerateMatchingCommandHandling);
            this.AddNoGoDateCommand = new RelayCommand(this.AddNoGoDateCommandHanding);
            this.DeleteMentorsCommand = new RelayCommand(this.DeleteMentorsCommandHandling);
            this.DeleteMenteesCommand = new RelayCommand(this.DeleteMenteesCommandHandling);
            this.DeleteAllDataCommand = new RelayCommand(this.DeleteAllDataCommandHandling);
            this.ShowInfoCommand = new RelayCommand(this.ShowInfoCommandHandling);
            this.ShowNoGoDatesCommand = new RelayCommand(this.ShowNoGoDatesCommandHandling);

            this.OnLoadedCommand = new RelayCommand(this.OnLoadedCommandHandling);

            this.AddNewMentorCommand = new RelayCommand(this.AddNewMentorCommandHandling);
            this.AddNewMenteeCommand = new RelayCommand(this.AddNewMenteeCommandHandling);
            this.GenerateMatchingWithNoGoDatesCommand = new RelayCommand(
                this.GenerateMatchingWithNoGoDatesCommandHandling, this.CanExecuteGenerateMatchingCommandHandling);

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

        private void AddNewMenteeCommandHandling()
        {
            this.Mentees.Add(new Mentee());
        }

        private void AddNewMentorCommandHandling()
        {
            this.Mentors.Add(new Mentor());
        }

        private void ShowNoGoDatesCommandHandling()
        {
            if (this.NoGoDates != null)
            {
                WindowManager.ShowNoGoDatesWindow(this.NoGoDates);
            }
        }

        private void AddNoGoDateCommandHanding()
        {
            if (this.NoGoMentor == null || this.NoGoMentee == null)
            {
                MessageBox.Show("Bitte eine Mentorin und eine Mentee für das NoGo-Date auswählen.", "Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //TODO UNSCHÖN
            if (String.IsNullOrEmpty(this.NoGoMentor.Name)
                || String.IsNullOrEmpty(this.NoGoMentor.Vorname)
                || String.IsNullOrEmpty(this.NoGoMentee.Name)
                || String.IsNullOrEmpty(this.NoGoMentee.Vorname))
            {
                MessageBox.Show(
                    "Ungültige Auswahl. Bitte eine Mentorin oder Mentee mit gültigem Namen und Vornamen wählen.",
                    "Information",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (this.NoGoDates.All(ngd => ngd.Item1 != this.NoGoMentor || ngd.Item2 != this.NoGoMentee))
            {
                this.NoGoDates.Add((this.NoGoMentor, this.NoGoMentee));
            }
        }

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
            this.PreMatchingGenerationValidationAndCorrectionRoutine();

            WindowManager.ShowMatchingWindow(this, false, this.maxDruckSpalten);
        }

        private void GenerateMatchingWithNoGoDatesCommandHandling()
        {
            this.PreMatchingGenerationValidationAndCorrectionRoutine();

            WindowManager.ShowMatchingWindow(this, true);
        }

        private void PreMatchingGenerationValidationAndCorrectionRoutine()
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

            this.ValidateMentorAndMenteeLists();
        }

        internal void ValidateMentorAndMenteeLists()
        {
            for (var i = this.Mentees.Count - 1; i >= 0; i--)
            {
                if (String.IsNullOrEmpty(this.Mentees[i].Name) || String.IsNullOrEmpty(this.Mentees[i].Vorname))
                {
                    this.Mentees.RemoveAt(i);
                }
            }

            for (var i = this.Mentors.Count - 1; i >= 0; i--)
            {
                if (String.IsNullOrEmpty(this.Mentors[i].Name) || String.IsNullOrEmpty(this.Mentors[i].Vorname))
                {
                    this.Mentors.RemoveAt(i);
                }
            }
        }

        private bool CanExecuteGenerateMatchingCommandHandling()
        {
            return this.Mentors.Any() && this.Mentees.Any() && !this.ValidationRulesHasError;
        }

        private void SaveCommandHandling()
        {
            var splits = this.AppSaveConfig.AppSaveFileName.Split('\\');
            var split = splits.FirstOrDefault(x => x.Contains(".json"));

            var sfd = new SaveFileDialog
            {
                InitialDirectory = this.AppSaveConfig.AppSaveFileFolder,
                FileName = split,
                Filter = "JSON Files(*.json) | *.json|All Files(*.*) | *.*",
                DefaultExt = "JSON Files (*.json) | .json",
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() != true)
                return;

            var jsonData = JsonConvert.SerializeObject(this, Formatting.Indented);
            this.AppSaveConfig.AppSaveFileName = sfd.FileName;
            this.AppSaveConfig.AppSaveFileFolder = Path.GetDirectoryName(sfd.FileName);
            File.WriteAllText(Path.Combine(this.AppSaveConfig.AppSaveFileFolder, this.AppSaveConfig.AppSaveFileName),
                jsonData);
        }

        private void OnLoadedCommandHandling()
        {
            #region Config/Default erstellen

            if (!Directory.Exists(this.AppSaveConfig.AppSaveFileFolder))
            {
                Directory.CreateDirectory(this.AppSaveConfig.AppSaveFileFolder);
            }

            if (!File.Exists(this.AppSaveConfig.AppConfigPath))
            {
                File.Create(this.AppSaveConfig.AppConfigPath).Close();
                var defaultConfig = new
                    {Folder = this.AppSaveConfig.AppSaveFileFolder, FileName = this.AppSaveConfig.AppSaveFileName};
                var jsonConfig = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
                File.WriteAllText(this.AppSaveConfig.AppConfigPath, jsonConfig);
            }

            var fileContent = File.ReadAllText(this.AppSaveConfig.AppConfigPath);
            if (String.IsNullOrEmpty(fileContent))
            {
                var defaultConfig = new
                    {Folder = this.AppSaveConfig.AppSaveFileFolder, FileName = this.AppSaveConfig.AppSaveFileName};
                var jsonConfig = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
                File.WriteAllText(this.AppSaveConfig.AppConfigPath, jsonConfig);
            }

            #endregion

            #region Config laden

            var configDefinition = new {Folder = "", FileName = ""};

            var savedConfig = File.ReadAllText(this.AppSaveConfig.AppConfigPath);
            var savedConfigDeserialzed = JsonConvert.DeserializeAnonymousType(savedConfig, configDefinition);

            if (savedConfigDeserialzed == null)
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
                Mentors = new List<Mentor>(),
                NoGoDates = new List<(Mentor, Mentee)>()
            };

            if (!File.Exists(this.AppSaveConfig.CombineAppPaths()))
                return;
            var jsonData = File.ReadAllText(Path.Combine(this.AppSaveConfig.AppSaveFileFolder,
                this.AppSaveConfig.AppSaveFileName));
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

            if (deserializedJson.NoGoDates == null)
                return;

            foreach (var noGoDate in deserializedJson.NoGoDates)
            {
                this.NoGoDates.Add(noGoDate);
            }

            #endregion
        }

        private void ShowInfoCommandHandling()
        {
            var sb = new StringBuilder();
            var versionMajor = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major;
            var versionMinor = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor;
            var versionBuild = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;
            sb.Append(versionMajor);
            sb.Append(".");
            sb.Append(versionMinor);
            sb.Append(".");
            sb.Append(versionBuild);
            var versionNo = sb.ToString();
            var msg = "Version " + versionNo +
                      "\nDiese App wurde vom HÄVGRZ-Alphateam entwickelt.\nhttps://github.com/haevg-rz/MentorSpeedDatingApp";
            MessageBox.Show(msg,
                "App-Informationen");
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
                {Folder = this.AppSaveConfig.AppSaveFileFolder, FileName = this.AppSaveConfig.AppSaveFileName};
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
                Mentors = new List<Mentor>(),
                NoGoDates = new List<(Mentor, Mentee)>()
            };

            if (!File.Exists(Path.Combine(this.AppSaveConfig.AppSaveFileFolder, this.AppSaveConfig.AppSaveFileName)))
            {
                File.Create(Path.Combine(this.AppSaveConfig.AppSaveFileFolder, this.AppSaveConfig.AppSaveFileName))
                    .Close();
            }

            var jsonData = File.ReadAllText(Path.Combine(this.AppSaveConfig.AppSaveFileFolder,
                this.AppSaveConfig.AppSaveFileName));

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
                   || this.EndTimeMinutes != deserializedJson.EndTimeMinutes
                   || this.CheckNoGoDatesForChanges(deserializedJson.NoGoDates);
        }

        private bool CheckNoGoDatesForChanges(List<(Mentor, Mentee)> noGoDates)
        {
            if (this.NoGoDates.Any() && noGoDates == null)
                return true;

            if (!this.NoGoDates.Any() && noGoDates == null)
                return false;

            if (this.NoGoDates.Count != noGoDates.Count)
                return true;

            return !this.NoGoDates.CompareCollectionsOnEqualContent(noGoDates, (tuple, valueTuple) =>
                tuple.Item1.Name == valueTuple.Item1.Name
                && tuple.Item1.Vorname == valueTuple.Item1.Vorname
                && tuple.Item1.Titel == valueTuple.Item1.Titel
                && tuple.Item2.Name == valueTuple.Item2.Name
                && tuple.Item2.Vorname == valueTuple.Item2.Vorname
                && tuple.Item2.Titel == valueTuple.Item2.Titel);
        }

        #endregion
    }
}