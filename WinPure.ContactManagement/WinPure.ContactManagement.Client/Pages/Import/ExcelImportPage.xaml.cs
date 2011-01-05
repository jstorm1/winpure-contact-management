using System.Windows;

namespace WinPure.ContactManagement.Client.Pages.Import
{
    /// <summary>
    /// Interaction logic for ExcelImportPage.xaml
    /// </summary>
    public partial class ExcelImportPage
    {
        public ExcelImportPage()
        {
            InitializeComponent();
        }

        private void onWizardControlCancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void onWizardControlFinishButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}