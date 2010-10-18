#region References

using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Pages.Modal;

#endregion

namespace WinPure.ContactManagement.Client.Pages
{
    /// <summary>
    /// Interaction logic for CompaniesListPage.xaml
    /// </summary>
    public partial class CompaniesListPage
    {
        /// <summary>
        /// Fires when user delete contacts by pressing Delete key.
        /// </summary>
        public event EventHandler DeleteEvent;

        public CompaniesListPage()
        {
            InitializeComponent();
            Loaded += delegate
            {
                DefaultViewButton.IsChecked = true;
            };
        }

        private void onAddButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CompaniesEditor();
            ModalDialog = dialog;
            dialog.Closed += onEditDialogClosed;
            dialog.Show();
        }

        private void onEditButtonClick(object sender, RoutedEventArgs e)
        {
            if (CompaniesListView.SelectedItem == null) return;
            var dialog = new CompaniesEditor((Company) CompaniesListView.SelectedItem);
            ModalDialog = dialog;
            dialog.Closed += onEditDialogClosed;
            dialog.Show();
        }

        private void onCompaniesListMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CompaniesListView.SelectedItem == null || e.ChangedButton != MouseButton.Left) return;

            var dialog = new CompaniesEditor((Company) CompaniesListView.SelectedItem);
            ModalDialog = dialog;
            dialog.Closed += onEditDialogClosed;
            dialog.Show();
        }

        private void onEditDialogClosed(object sender, EventArgs e)
        {
            SearchBox.Text = "";
        }

        private void onDefaultViewButtonChecked(object sender, RoutedEventArgs e)
        {
            CompaniesListView.View = CompaniesListView.FindResource("DefaultView") as ViewBase;
            CompaniesListView.Background = null;
            DataGridView.Visibility = Visibility.Collapsed;
            Sort.Visibility = Visibility.Visible;
        }

        private void onListViewButtonChecked(object sender, RoutedEventArgs e)
        {
            CompaniesListView.View = CompaniesListView.FindResource("PlainView") as ViewBase;
            CompaniesListView.Background = null;
            DataGridView.Visibility = Visibility.Collapsed;
            Sort.Visibility = Visibility.Visible;
        }

        private void onGridViewButtonChecked(object sender, RoutedEventArgs e)
        {
            DataGridView.Visibility = Visibility.Visible;
            Sort.Visibility = Visibility.Collapsed;
        }

        private void onCompaniesListViewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                IList s = CompaniesListView.SelectedItems;
                DeleteButton.Command.Execute(s);
            }
        }

        private void onDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (DataGridView.Visibility == Visibility.Visible)
            {
                DeleteButton.CommandParameter = DataGridView.SelectedItems;
            }

            DeleteButton.CommandParameter = CompaniesListView.SelectedItems;
        }

        private void onDataGridViewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridView.SelectedItem == null || e.ChangedButton != MouseButton.Left) return;

            var dialog = new CompaniesEditor((Company)DataGridView.SelectedItem);
            ModalDialog = dialog;
            dialog.Closed += onEditDialogClosed;
            dialog.Show();
        }

        private void onDataGridViewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                IList s = DataGridView.SelectedItems;
                DeleteButton.Command.Execute(s);
            }
        }
    }
}