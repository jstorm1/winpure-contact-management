#region References 

using System.Windows;
using System.Windows.Threading;
using WinPure.ContactManagement.Client.Services;
using WinPure.ContactManagement.Client.Services.Managers;
using WinPure.ContactManagement.Common;
using WinPure.ContactManagement.Common.SyncServiceHelpers;

#endregion

namespace WinPure.ContactManagement.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            DispatcherUnhandledException += App_DispatcherUnhandledException;

            //Prepare db for synchronization.
            if (!ScopeHelper.CheckScope(Constants.LocalConnectionString))
            {
                ScopeHelper.CreateScope(Constants.LocalConnectionString);
            }
            
            base.OnStartup(e);

            SyncServiceManager.Current.RunService();
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        
    }
}