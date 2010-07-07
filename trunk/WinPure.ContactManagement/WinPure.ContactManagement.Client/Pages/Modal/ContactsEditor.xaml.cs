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

namespace WinPure.ContactManagement.Client.Pages.Modal
{
    /// <summary>
    /// Interaction logic for ContactsEditor.xaml
    /// </summary>
    public partial class ContactsEditor : ModalDialog
    {
        public ContactsEditor()
        {
            InitializeComponent();
        }

        private void onSaveButtonClick(object sender, RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
        }

        private void onCancelButtonClick(object sender, RoutedEventArgs e)
        {
        	Close();
        }
    }
}
