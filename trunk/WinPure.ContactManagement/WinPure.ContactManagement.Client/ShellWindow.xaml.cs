#region References

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using WinPure.ContactManagement.Client.Data.Synchronization;
using WinPure.ContactManagement.Client.Services;
using WinPure.ContactManagement.Common;
using WinPure.ContactManagement.Common.SyncServiceHelpers;

#endregion

namespace WinPure.ContactManagement.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<EndpointAddress> a = SyncServiceManager.Current.GetAddressesOfService();
            string str = "SyncService instances in Network:\n";
            foreach (EndpointAddress endpointAddress in a)
            {
                str += endpointAddress + "\n";
            }
            MessageBox.Show(str);

            SynchronizationManager.Synchronize(a.FirstOrDefault());
        }

      
    }
}