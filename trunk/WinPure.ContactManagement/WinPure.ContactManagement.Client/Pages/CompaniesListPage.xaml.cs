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
using WinPure.ContactManagement.Client.Pages.Modal;

namespace WinPure.ContactManagement.Client.Pages
{
    /// <summary>
    /// Interaction logic for CompaniesListPage.xaml
    /// </summary>
    public partial class CompaniesListPage : PageControl
    {
        public CompaniesListPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //var page = new ContactsListPage();
            //ShowModalPage(page);

            var dialog = new CompaniesEditor();
            this.ModalDialog = dialog;
            dialog.Show();
        }
    }
}
