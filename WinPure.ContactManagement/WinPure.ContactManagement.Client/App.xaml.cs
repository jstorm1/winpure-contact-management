#region References 

using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Helpers;
using WinPure.ContactManagement.Client.Localization;
using WinPure.ContactManagement.Client.Services.Managers;
using WinPure.ContactManagement.Common;
using WinPure.ContactManagement.Common.SyncServiceHelpers;

#endregion

namespace WinPure.ContactManagement.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {

            LanguageDictionary.RegisterDictionary(
            CultureInfo.GetCultureInfo("en-US"),
            new XmlLanguageDictionary("Languages/en-US.xml"));

            LanguageDictionary.RegisterDictionary(
                CultureInfo.GetCultureInfo("es-ES"),
                new XmlLanguageDictionary("Languages/es-ES.xml"));

            LanguageDictionary.RegisterDictionary(
                CultureInfo.GetCultureInfo("fr-FR"),
                new XmlLanguageDictionary("Languages/fr-FR.xml"));

            LanguageDictionary.RegisterDictionary(
                CultureInfo.GetCultureInfo("it-IT"),
                new XmlLanguageDictionary("Languages/it-IT.xml"));

            var culture = CurrentCultureManager.Current.CurrentCultureName;
            culture = string.IsNullOrEmpty(culture) ? "en-US" : culture;
            LanguageContext.Current.Culture = CultureInfo.GetCultureInfo(culture);	


            DispatcherUnhandledException += onDispatcherUnhandledException;

            //Prepare db for synchronization.
            if (!ScopeHelper.CheckScope(Constants.LocalConnectionString))
            {
                ScopeHelper.CreateScope(Constants.LocalConnectionString);
            }
            
            base.OnStartup(e);

            SyncServiceManager.Current.RunService();
        }

        static void onDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        
    }
}