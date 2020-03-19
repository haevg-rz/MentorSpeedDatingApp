using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.ViewModel;

namespace MentorSpeedDatingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGrid_OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                IPerson person = e.Row.DataContext as IPerson;
                person.Save();
            }
        }

        private void DataGrid_OnPreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == DataGrid.DeleteCommand)
            {
                if (MessageBox.Show("Sind Sie sicher, dass sie die Personen löschen wollen?", "Bitte bestätigen!", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
