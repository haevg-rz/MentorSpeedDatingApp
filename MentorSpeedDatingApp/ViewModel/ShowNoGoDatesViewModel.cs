using GalaSoft.MvvmLight;
using MentorSpeedDatingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentorSpeedDatingApp.ViewModel
{
    public class ShowNoGoDatesViewModel : ViewModelBase
    {
        public List<(Mentor, Mentee)> NoGoDates { get; set; }

        public ShowNoGoDatesViewModel(List<(Mentor, Mentee)> noGoDates)
        {
            this.NoGoDates = noGoDates;
        }
    }
}
