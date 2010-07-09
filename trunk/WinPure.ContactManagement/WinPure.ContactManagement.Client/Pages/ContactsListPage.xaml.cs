using System.Windows;
using WinPure.ContactManagement.Client.CommonControls;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Pages.Modal;

namespace WinPure.ContactManagement.Client.Pages
{
    /// <summary>
    /// Interaction logic for ContactsListPage.xaml
    /// </summary>
    public partial class ContactsListPage : PageControl
    {
        public ContactsListPage()
        {
            InitializeComponent();
        }

        private void onAddButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ContactsEditor();
            ModalDialog = dialog;
            dialog.Show();
        }

        private void onEditButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ContactsEditor((Contact) ContactsList.SelectedItem);
            ModalDialog = dialog;
            dialog.Show();
        }
    }
}