#region References

using System;
using System.Windows;
using System.Windows.Input;
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
        public CompaniesListPage()
        {
            InitializeComponent();
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
            if (CompaniesList.SelectedItem == null) return;
            var dialog = new CompaniesEditor((Company) CompaniesList.SelectedItem);
            ModalDialog = dialog;
            dialog.Closed += onEditDialogClosed;
            dialog.Show();
        }

        private void onCompaniesListMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CompaniesList.SelectedItem == null || e.ChangedButton != MouseButton.Left) return;

            var dialog = new CompaniesEditor((Company) CompaniesList.SelectedItem);
            ModalDialog = dialog;
            dialog.Closed += onEditDialogClosed;
            dialog.Show();
        }

        private void onEditDialogClosed(object sender, EventArgs e)
        {
            SearchBox.Text = "";
        }
    }
}