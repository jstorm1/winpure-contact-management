#region References 

using System.Windows;
using WinPure.ContactManagement.Client.Services;
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

            DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);

            //Prepare db for synchronization.
            if (!ScopeHelper.CheckScope(Constants.LocalConnectionString))
            {
                ScopeHelper.CreateScope(Constants.LocalConnectionString);
            }
            
            base.OnStartup(e);

            SyncServiceManager.Current.RunService();
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        
    }
}