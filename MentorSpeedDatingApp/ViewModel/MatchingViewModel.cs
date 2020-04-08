using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using GalaSoft.MvvmLight;
using MentorSpeedDatingApp.Models;

namespace MentorSpeedDatingApp.ViewModel
{
    public class MatchingViewModel : ViewModelBase
    {
        public ObservableCollection<MatchingModel> Matchings { get; set; }
        public Dictionary<string, string> Data { get; set; }



        private string titel;
        public string Titel
        {
            get => this.titel;
            set => base.Set(ref this.titel, value);
        }

        public MatchingViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.Matchings = new ObservableCollection<MatchingModel>()
                {
                    new MatchingModel()
                    {
                        MatchedMentee = new Mentee()
                            {Name = "TestName1", Titel = "TestTitel1", Vorname = "TestVorname1"},
                        MatchedTime = new DateTime(2020, 4, 7, 7, 30, 0),
                        MatchedMentor = new Mentor()
                            {Name = "TestName1", Titel = "TestTitel1", Vorname = "TestVorname1"}
                    },
                    new MatchingModel()
                    {
                        MatchedMentee = new Mentee()
                            {Name = "TestName2", Titel = "TestTitel2", Vorname = "TestVorname2"},
                        MatchedTime = new DateTime(2020, 4, 7, 8, 0, 0),
                        MatchedMentor = new Mentor()
                            {Name = "TestName2", Titel = "TestTitel2", Vorname = "TestVorname2"}
                    },
                    new MatchingModel()
                    {
                        MatchedMentee = new Mentee()
                            {Name = "TestName3", Titel = "TestTitel3", Vorname = "TestVorname3"},
                        MatchedTime = new DateTime(2020, 4, 7, 8, 30, 0),
                        MatchedMentor = new Mentor()
                            {Name = "TestName3", Titel = "TestTitel3", Vorname = "TestVorname3"}
                    },
                    new MatchingModel()
                    {
                        MatchedMentee = new Mentee()
                            {Name = "TestName4", Titel = "TestTitel4", Vorname = "TestVorname4"},
                        MatchedTime = new DateTime(2020, 4, 7, 9, 0, 0),
                        MatchedMentor = new Mentor()
                            {Name = "TestName4", Titel = "TestTitel4", Vorname = "TestVorname4"}
                    }
                };
                this.Titel = "Speed Dating 2020 - Übersicht";
            }

            ;
        }
    }
}

public class MatchingModel : ViewModelBase
{
    private Mentor matchedMentor;

    public Mentor MatchedMentor
    {
        get => this.matchedMentor;
        set => base.Set(ref this.matchedMentor, value);
    }

    private Mentee matchedMentee;

    public Mentee MatchedMentee
    {
        get => this.matchedMentee;
        set => base.Set(ref this.matchedMentee, value);
    }

    private DateTime matchedTime;

    public DateTime MatchedTime
    {
        get => this.matchedTime;
        set => base.Set(ref this.matchedTime, value);
    }
}