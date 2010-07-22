﻿#region References

using System.Windows;
using WinPure.ContactManagement.Client.CommonControls;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Pages.Modal; 

#endregion

namespace WinPure.ContactManagement.Client.Pages
{
    /// <summary>
    /// Interaction logic for CompaniesListPage.xaml
    /// </summary>
    public partial class CompaniesListPage : PageControl
    {
        public CompaniesListPage()
        {
            InitializeComponent();
        }

        private void onAddButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CompaniesEditor();
            ModalDialog = dialog;
            dialog.Show();
        }

        private void onEditButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new CompaniesEditor((Company) CompaniesList.SelectedItem);
            ModalDialog = dialog;
            dialog.Show();
        }
    }
}
