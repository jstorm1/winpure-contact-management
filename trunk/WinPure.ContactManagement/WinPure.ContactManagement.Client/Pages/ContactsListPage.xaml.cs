﻿using System;
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
    /// Interaction logic for ContactsListPage.xaml
    /// </summary>
    public partial class ContactsListPage : PageControl
    {
        public ContactsListPage()
        {
            InitializeComponent();
        }

        private void onAddButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ContactsEditor();
            ModalDialog = dialog;
            dialog.Show();
        }
    }
}
