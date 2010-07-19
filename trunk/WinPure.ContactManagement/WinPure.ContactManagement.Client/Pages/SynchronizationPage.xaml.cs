using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinPure.ContactManagement.Client.CommonControls;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Pages.Modal;

namespace WinPure.ContactManagement.Client.Pages
{
    /// <summary>
    /// Interaction logic for SynchronizationPage.xaml
    /// </summary>
    public partial class SynchronizationPage : PageControl
    {
        public SynchronizationPage()
        {
            InitializeComponent();
        }

        private void onAddButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new  SyncServerConnectionEditor();
            ModalDialog = dialog;
            dialog.Show();
        }

        private void onEditButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	var dialog = new  SyncServerConnectionEditor((SyncServerConnection) SyncServersList.SelectedItem);
            ModalDialog = dialog;
            dialog.Show();
        }
    }
}
