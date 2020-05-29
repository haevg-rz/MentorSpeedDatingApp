using System.ComponentModel;
using System.Windows;

namespace MentorSpeedDatingApp.Views
{
    /// <summary>
    /// Interaktionslogik für MatchingWindow.xaml
    /// </summary>
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
