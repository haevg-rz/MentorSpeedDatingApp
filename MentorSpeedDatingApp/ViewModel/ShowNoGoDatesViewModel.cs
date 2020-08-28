using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using MentorSpeedDatingApp.Models;
using System.Collections.ObjectModel;

namespace MentorSpeedDatingApp.ViewModel
{
    public class ShowNoGoDatesViewModel : ViewModelBase
    {
        public ObservableCollection<(Mentor, Mentee)> NoGoDates { get; set; }
        public RelayCommand DeleteNoGoDateCommand { get; set; }

        private (Mentor, Mentee) selectedNoGoDate;

        public (Mentor, Mentee) SelectedNoGoDate
        {
            get => this.selectedNoGoDate; 
            set => base.Set(ref this.selectedNoGoDate, value);
        }

        [PreferredConstructor]
        public ShowNoGoDatesViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.NoGoDates = new ObservableCollection<(Mentor, Mentee)>
                {
                    (new Mentor {Name = "Müller", Vorname = "Steffie", Titel = "Dr."},
                        new Mentee {Name = "Braun", Vorname = "Sandra", Titel = ""}),
                    (new Mentor {Name = "Jansen", Vorname = "Elizabeth", Titel = ""},
                        new Mentee {Name = "Braun", Vorname = "Katrin", Titel = ""}),
                };
            }
        }

        public ShowNoGoDatesViewModel(ObservableCollection<(Mentor, Mentee)> noGoDates)
        {
            this.NoGoDates = noGoDates;
            this.DeleteNoGoDateCommand = new RelayCommand(this.DeleteNoGoDateCommandHandling);
        }

        private void DeleteNoGoDateCommandHandling()
        {
            this.NoGoDates.Remove(this.SelectedNoGoDate);
            this.SelectedNoGoDate = (null, null);
        }
    }
}