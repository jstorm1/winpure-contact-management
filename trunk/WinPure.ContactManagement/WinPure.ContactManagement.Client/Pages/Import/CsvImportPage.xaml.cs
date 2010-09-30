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

namespace WinPure.ContactManagement.Client.Pages.Import
{
    /// <summary>
    /// Interaction logic for CsvImportPage.xaml
    /// </summary>
    public partial class CsvImportPage 
    {
        public CsvImportPage()
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
