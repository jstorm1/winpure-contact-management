#region MyRegion

using WinPure.ContactManagement.Client.CommonControls;

#endregion

namespace WinPure.ContactManagement.Client.Pages.Modal
{
    /// <summary>
    /// Interaction logic for CompaniesEditor.xaml
    /// </summary>
    public partial class CompaniesEditor : ModalDialog
    {
        public CompaniesEditor()
        {
            InitializeComponent();
        }

        private void onCancelButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	Close();
        }
    }
}