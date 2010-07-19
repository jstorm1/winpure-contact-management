#region References 

using System.Windows;
using WinPure.ContactManagement.Client.CommonControls;
using WinPure.ContactManagement.Client.Data.Domains;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.ViewModels;

#endregion

namespace WinPure.ContactManagement.Client.Pages.Modal
{
    /// <summary>
    /// Interaction logic for SyncServerConnectionEditor.xaml
    /// </summary>
    public partial class SyncServerConnectionEditor : ModalDialog
    {
        private readonly SyncServerConnection _syncServerConnection;

        public SyncServerConnectionEditor(SyncServerConnection connection = null)
        {
            InitializeComponent();

            _syncServerConnection = connection ?? new SyncServerConnection();
            DataContext = new SyncServerConnectionEditorViewModel(new SmoTasks(), _syncServerConnection.ConnectionString)
                              {Connection = _syncServerConnection};
        }

        private void onCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void onSaveButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}