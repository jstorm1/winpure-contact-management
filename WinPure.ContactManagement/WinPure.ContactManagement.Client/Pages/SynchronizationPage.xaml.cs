#region References

using System.Windows;
using System.Windows.Input;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Pages.Modal;

#endregion

namespace WinPure.ContactManagement.Client.Pages
{
    /// <summary>
    /// Interaction logic for SynchronizationPage.xaml
    /// </summary>
    public partial class SynchronizationPage
    {
        public SynchronizationPage()
        {
            InitializeComponent();
        }

        private void onAddButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SyncServerConnectionEditor();
            ModalDialog = dialog;
            dialog.Show();
        }

        private void onEditButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SyncServerConnectionEditor((SyncServerConnection) SyncServersList.SelectedItem);
            ModalDialog = dialog;
            dialog.Show();
        }

        private void onSyncServersListMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SyncServersList.SelectedItem == null || e.ChangedButton != MouseButton.Left) return;

            var dialog = new SyncServerConnectionEditor((SyncServerConnection) SyncServersList.SelectedItem);
            ModalDialog = dialog;
            dialog.Show();
        }
    }
}