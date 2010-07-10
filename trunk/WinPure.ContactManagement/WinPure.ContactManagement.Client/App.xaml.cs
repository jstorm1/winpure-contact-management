using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Windows;
using WinPure.ContactManagement.Client.Services;
using WinPure.ContactManagement.Client.SyncService;

namespace WinPure.ContactManagement.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //SyncServiceManager.Current.RunService();
        }
    }
}
