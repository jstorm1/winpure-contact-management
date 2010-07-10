using System.Linq;
using System.ServiceModel;
using System.Windows;
using WinPure.ContactManagement.Client.Services;
using WinPure.ContactManagement.Client.SyncService;

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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var a = SyncServiceManager.Current.GetAdderessesOfService();
            string str = "SyncService instances in Network:\n";
            foreach (var endpointAddress in a)
            {
                str += endpointAddress + "\n";
            }
            MessageBox.Show(str);

            var binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.None;

            var client = new SyncServiceClient(binding, a.FirstOrDefault());
            var s = client.State;
             str = client.GetMessage("Hello WCF 4 ");

            MessageBox.Show(str);
        }
    }
}
