using MentorSpeedDatingApp.Models;
using System.Collections.Generic;
using System.Windows;

namespace MentorSpeedDatingApp.Views
{
    public partial class MatchingWindow : Window
    {
        public MatchingWindow()
        {
            InitializeComponent();
        }

        public MatchingWindow(List<Mentor> mentorList)
        {
            this.InitializeComponent();
        }
    }
}