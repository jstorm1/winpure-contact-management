using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Win32;
using WinPure.ContactManagement.Client.Localization;
using Application = Microsoft.Office.Interop.Outlook.Application;

namespace WinPure.ContactManagement.Client.Data.Managers.Import
{
    public class OutlookImportManager
    {
        private _Application _outlookApplication;

        #region Singleton Constructor

        private static OutlookImportManager _instance;


        private OutlookImportManager()
        {
            initialize();
        }

        public static OutlookImportManager Current
        {
            get { return _instance ?? (_instance = new OutlookImportManager()); }
        }

        #endregion

        #region Helper Methods

        private void initialize(bool restartOultlook = false)
        {
            if (restartOultlook)
                restartOutlookInstance();

            try
            {
                _outlookApplication = new Application();
            }
            catch (COMException e)
            {
                const string messsage =
                    @"HRESULT: 0x80080005";
                
                if (!e.Message.Contains(messsage))
                    throw;

                var result = MessageBox.Show(LanguageDictionary.CurrentDictionary.Translate<string>("Messages.OutlookProblem", "Message"),
                    LanguageDictionary.CurrentDictionary.Translate<string>("Messages.OutlookProblem", "Title"), 
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.No) return;
                initialize(true);
            }
        }


        private static void restartOutlookInstance()
        {
            Process[] processes = Process.GetProcessesByName("OUTLOOK");
            foreach (Process process in processes)
            {
                process.Kill();
            }

            string path = getOutlookPath();

            Process.Start(path);
        }

        private static string getOutlookPath()
        {
            string path = "";

            RegistryKey key =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\OUTLOOK.EXE",
                                                 false);
            if (key != null)
            {
                path = (string) key.GetValue("Path");
            }
            return path + "OUTLOOK.EXE";
        }

        #endregion

        public Folders GetContactsFolders()
        {
            MAPIFolder contactsFolder = _outlookApplication.Session.GetDefaultFolder(OlDefaultFolders.olFolderContacts);
            return contactsFolder.Folders;
        }

        public Items GetContactsFromFolder(Folder folder)
        {
            return folder.Items;
        }
    }
}