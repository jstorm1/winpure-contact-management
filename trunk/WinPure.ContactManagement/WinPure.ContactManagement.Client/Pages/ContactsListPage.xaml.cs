#region References

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WinPure.ContactManagement.Client.CommonControls;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Pages.Import.Outlook;
using WinPure.ContactManagement.Client.Pages.Modal;

#endregion

namespace WinPure.ContactManagement.Client.Pages
{
    /// <summary>
    /// Interaction logic for ContactsListPage.xaml
    /// </summary>
    public partial class ContactsListPage
    {
        public ContactsListPage()
        {
            InitializeComponent();
            Loaded += delegate
                          {
                              DefaultViewButton.IsChecked = true;
                          };
        }

        private void onAddButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ContactsEditor();
            ModalDialog = dialog;
            dialog.Closed += onEditDialogClosed;
            dialog.Show();
        }

        private void onEditButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ContactsEditor((Contact)ContactsListView.SelectedItem);
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
            ContactsListView.View = ContactsListView.FindResource("DefaultView") as ViewBase;
        }

        private void onListViewButtonChecked(object sender, RoutedEventArgs e)
        {

            ContactsListView.View = ContactsListView.FindResource("PlainView") as ViewBase;

        }

        private void onGridViewButtonChecked(object sender, RoutedEventArgs e)
        {
            ContactsListView.View = ContactsListView.FindResource("GridView") as ViewBase;
        }

        private void onContactsListViewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ContactsListView.SelectedItem == null || e.ChangedButton != MouseButton.Left) return;

            var dialog = new ContactsEditor((Contact)ContactsListView.SelectedItem);
            ModalDialog = dialog;
            dialog.Closed += onEditDialogClosed;
            dialog.Show();
        }

        private void onOutlookImportButtonClick(object sender, RoutedEventArgs e)
        {
            var wizard = new OutlookImportWizard();
            ModalDialog = wizard;
            wizard.Sequence.Enqueue(new PreviewScreen(wizard));
            wizard.Sequence.Enqueue(new OutlookProgressScreen(wizard));
            wizard.ShowNext(wizard.GetNextScreen());
            wizard.Show();
        }
    }
}