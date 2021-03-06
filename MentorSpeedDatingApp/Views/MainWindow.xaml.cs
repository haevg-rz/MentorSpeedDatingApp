using GalaSoft.MvvmLight.Ioc;
using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.ViewModel;
using MentorSpeedDatingApp.WindowManagement;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            if ((File.GetAttributes(Path.Combine(
                     SimpleIoc.Default.GetInstance<MainViewModel>().AppSaveConfig.AppSaveFileFolder,
                     SimpleIoc.Default.GetInstance<MainViewModel>().AppSaveConfig.AppSaveFileName)) &
                 FileAttributes.Temporary) == FileAttributes.Temporary)
            {
                File.Delete(Path.Combine(SimpleIoc.Default.GetInstance<MainViewModel>().AppSaveConfig.AppSaveFileFolder,
                    SimpleIoc.Default.GetInstance<MainViewModel>().AppSaveConfig.AppSaveFileName));
            }

            ViewModelLocator.Cleanup();
            WindowManager.CloseAllWindows();
        }


        private void MentorsGriView_KeyDownEventHandler(object sender, KeyEventArgs e)
        {
            if (!(sender is DataGridRow selectedRow))
                return;

            if (!selectedRow.Item.Equals(this.MentorsGridView.Items[^1]))
                return;

            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                var mentorsList = SimpleIoc.Default.GetInstance<MainViewModel>().Mentors;
                if (!String.IsNullOrEmpty(mentorsList.Last().Name) && !String.IsNullOrEmpty(mentorsList.Last().Vorname))
                {
                    mentorsList.Add(new Mentor());
                    e.Handled = true;
                }
            }
        }

        private void MenteesGriView_KeyDownEventHandler(object sender, KeyEventArgs e)
        {
            if (!(sender is DataGridRow selectedRow))
                return;

            if (!selectedRow.Item.Equals(this.MenteesGridView.Items[^1]))
                return;

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                var menteesList = SimpleIoc.Default.GetInstance<MainViewModel>().Mentees;
                if (!String.IsNullOrEmpty(menteesList.Last().Name) && !String.IsNullOrEmpty(menteesList.Last().Vorname))
                {
                    menteesList.Add(new Mentee());
                    e.Handled = true;
                }
            }
        }
    }
}