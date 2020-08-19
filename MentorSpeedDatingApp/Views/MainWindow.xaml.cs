using GalaSoft.MvvmLight.Ioc;
using MentorSpeedDatingApp.Models;
using MentorSpeedDatingApp.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace MentorSpeedDatingApp.Views
{
    public partial class MainWindow : Window
    {
        private Control control;

        private TabKeyActionControl tabKeyActionControl = TabKeyActionControl.BeginEditOnSelectedCell;

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

        private void PreviewKeyDown_EventHandler(object sender, KeyEventArgs e)
        {
            if (!(sender is DataGridRow selectedRow))
                return;

            switch (e.Key)
            {
                case Key.Tab:
                    switch (this.MentorsGridView.CurrentCell.Column.DisplayIndex)
                    {
                        case 0:
                            if (this.tabKeyActionControl == TabKeyActionControl.BeginEditOnSelectedCell)
                            {
                                this.control.Focus();
                                this.tabKeyActionControl = TabKeyActionControl.SwitchSelectedCell;
                            }
                            else
                            {
                                this.MentorsGridView.CurrentCell = new DataGridCellInfo(
                                    this.MentorsGridView.Items[this.MentorsGridView.Items.IndexOf(selectedRow.Item)],
                                    this.MentorsGridView.Columns[1]);
                                this.tabKeyActionControl = TabKeyActionControl.BeginEditOnSelectedCell;
                            }
                            break;
                        case 1:
                            if (this.tabKeyActionControl == TabKeyActionControl.BeginEditOnSelectedCell)
                            {
                                this.control.Focus();
                                this.tabKeyActionControl = TabKeyActionControl.SwitchSelectedCell;
                            }
                            else
                            {
                                this.MentorsGridView.CurrentCell = new DataGridCellInfo(
                                this.MentorsGridView.Items[this.MentorsGridView.Items.IndexOf(selectedRow.Item)],
                                this.MentorsGridView.Columns[2]);
                                this.tabKeyActionControl = TabKeyActionControl.BeginEditOnSelectedCell;
                            }
                            break;
                        case 2:
                            this.control.Focus();
                            break;
                    }

                    
                    e.Handled = true;
                    break;

                case Key.Enter:
                    if (selectedRow.Item is Mentor mentor && !String.IsNullOrEmpty(mentor.Name) &&
                        !String.IsNullOrEmpty(mentor.Vorname))
                    {
                        var idx = this.MentorsGridView.Items.IndexOf(this.MentorsGridView.SelectedItem) + 1;
                        this.MentorsGridView.SelectedItem =
                            this.MentorsGridView.Items[idx];
                        this.MentorsGridView.CurrentCell = new DataGridCellInfo(
                            this.MentorsGridView.Items[idx], this.MentorsGridView.Columns[1]);
                    }

                    e.Handled = true;
                    break;

                case Key.Down:
                    if (this.MentorsGridView.SelectedItem.Equals(this.MentorsGridView.Items[^2]) 
                    && !String.IsNullOrEmpty((this.MentorsGridView.SelectedItem as Mentor).Name)
                    && !String.IsNullOrEmpty((this.MentorsGridView.SelectedItem as Mentor).Vorname))
                    {
                        var idx = this.MentorsGridView.Items.IndexOf(this.MentorsGridView.SelectedItem) + 1;
                        this.MentorsGridView.SelectedItem =
                            this.MentorsGridView.Items[idx];
                        this.MentorsGridView.CurrentCell = new DataGridCellInfo(
                            this.MentorsGridView.Items[idx], this.MentorsGridView.Columns[1]);
                    }
                    break;
            }
        }

        private void MentorsGridView_OnFocused(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                // Starts the Edit on the row;
                DataGrid grd = (DataGrid) sender;
                grd.BeginEdit(e);
                    
                this.control = this.GetFirstChildByType<Control>(e.OriginalSource as DataGridCell);
            }
        }

        private T GetFirstChildByType<T>(DependencyObject prop) where T : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(prop); i++)
            {
                var child = VisualTreeHelper.GetChild((prop), i);
                if (child is T castedProp)
                    return castedProp;

                castedProp = this.GetFirstChildByType<T>(child);

                if (castedProp != null)
                    return castedProp;
            }

            return null;
        }

        private enum TabKeyActionControl
        {
            SwitchSelectedCell,
            BeginEditOnSelectedCell
        }

        private void MentorsGridView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (this.MentorsGridView.SelectedCells.Count > 3)
            //{
            //    for (int i = this.MentorsGridView.SelectedCells.Count - 1; i >= 3; i--)
            //    {
            //        this.MentorsGridView.SelectedCells.RemoveAt(i);
            //    }
            //}
            

            if (this.MentorsGridView.SelectedItem == null || this.MentorsGridView.SelectedItem.Equals(CollectionView.NewItemPlaceholder))
                return;

            if (this.MentorsGridView.CurrentItem is Mentor m)
                if (String.IsNullOrEmpty(m.Name) || String.IsNullOrEmpty(m.Vorname))
                    return;

            foreach (var mentor in new List<Mentor>(SimpleIoc.Default.GetInstance<MainViewModel>().Mentors))
            {
                if (this.MentorsGridView.SelectedItem.Equals(mentor))
                    continue;

                if (String.IsNullOrEmpty(mentor.Name) || String.IsNullOrEmpty(mentor.Vorname))
                {
                    SimpleIoc.Default.GetInstance<MainViewModel>().Mentors.Remove(mentor);
                }
            }
        }
    }
}