using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using MentorSpeedDatingApp.ViewModel;

namespace MentorSpeedDatingApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            var userDecision = SimpleIoc.Default.GetInstance<MainViewModel>().OnCloseCommand();

            if (userDecision == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
                return;
            }

            ViewModelLocator.Cleanup();
        }
    }
}