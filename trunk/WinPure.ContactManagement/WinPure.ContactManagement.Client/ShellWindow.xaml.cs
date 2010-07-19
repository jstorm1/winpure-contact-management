#region References

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using WinPure.ContactManagement.Client.Data.Synchronization;
using WinPure.ContactManagement.Client.Services;
using WinPure.ContactManagement.Common;
using WinPure.ContactManagement.Common.Interfaces.Windows;
using WinPure.ContactManagement.Common.SyncServiceHelpers;

#endregion

namespace WinPure.ContactManagement.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window, IShellWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        public object ModalContent
        {
            set
            {
                FrontGrid.Children.Clear();
                //if (value != null)
                   // FrontGrid.Children.Add((UIElement)value);
            }
        }
    }
}