using System.Windows;

namespace WinPure.ContactManagement.Client.Pages.Import
{
    /// <summary>
    /// Interaction logic for OutlookImportPage.xaml
    /// </summary>
    public partial class OutlookImportPage
    {
        public OutlookImportPage()
        {
            InitializeComponent();
        }

        private void onWizardControlFinishButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void onWizardControlCancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}