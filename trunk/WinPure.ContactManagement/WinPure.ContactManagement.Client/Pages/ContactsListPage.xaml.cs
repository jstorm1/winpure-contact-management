#region References

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WinPure.ContactManagement.Client.CommonControls;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Pages.Import;
using WinPure.ContactManagement.Client.Pages.Import.Csv;
using WinPure.ContactManagement.Client.Pages.Import.Outlook;
using WinPure.ContactManagement.Client.Pages.Modal;
using PreviewScreen = WinPure.ContactManagement.Client.Pages.Import.Outlook.PreviewScreen;

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
            ContactsListView.Background = null;
        }

        private void onListViewButtonChecked(object sender, RoutedEventArgs e)
        {

            ContactsListView.View = ContactsListView.FindResource("PlainView") as ViewBase;
            ContactsListView.Background = null;
        }

        private void onGridViewButtonChecked(object sender, RoutedEventArgs e)
        {
            ContactsListView.View = ContactsListView.FindResource("GridView") as ViewBase;
            ContactsListView.Background = new SolidColorBrush(Colors.White);
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
            var wizard = new OutlookImportPage { Height = 480, Width = 640 };
            ModalDialog = wizard;
            wizard.Show();
        }

        private void onCsvImportButtonClick(object sender, RoutedEventArgs e)
        {
            var wizard = new CsvImportPage {Height = 480, Width = 640};
            ModalDialog = wizard;
            wizard.Show();
        }

        private void onExcelImportButtonClick(object sender, RoutedEventArgs e)
        {
            var wizard = new ExcelImportPage{ Height = 480, Width = 640 };
            ModalDialog = wizard;
            wizard.Show();
        }
    }
}