#region References

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WinPure.ContactManagement.Client.Data.Model;
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
            var dialog = new ContactsEditor((Contact) ContactsList.SelectedItem);
            ModalDialog = dialog;
            dialog.Closed += onEditDialogClosed;
            dialog.Show();
        }

        private void onContactsListMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ContactsList.SelectedItem == null || e.ChangedButton != MouseButton.Left) return;

            var dialog = new ContactsEditor((Contact) ContactsList.SelectedItem);
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
            listView.View = listView.FindResource("DefaultView") as ViewBase;
        }

        private void onListViewButtonChecked(object sender, RoutedEventArgs e)
        {
            listView.View = listView.FindResource("PlainView") as ViewBase;
        }

        private void onGridViewButtonChecked(object sender, RoutedEventArgs e)
        {
            listView.View = listView.FindResource("GridView") as ViewBase;
        }
    }
}