using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Win32;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Localization;
using Application = Microsoft.Office.Interop.Outlook.Application;

namespace WinPure.ContactManagement.Client.Data.Managers.Import
{
    public class OutlookImportManager
    {
        private readonly MAPIFolder _contactsFolder;
        private readonly BackgroundWorker _importWorker;
        private _Application _outlookApplication;

        #region Singleton Constructor

        private static OutlookImportManager _instance;
        private ObservableCollection<object> _contactsFromFolder;

        private OutlookImportManager()
        {
            initialize();
            _contactsFolder =
                _outlookApplication.ActiveExplorer().Session.GetDefaultFolder(OlDefaultFolders.olFolderContacts);

            _importWorker = new BackgroundWorker();
            _importWorker.WorkerReportsProgress = true;
            _importWorker.DoWork += onImportWorkerOnDoWork;
            _importWorker.RunWorkerCompleted += onImportWorkerOnRunWorkerCompleted;
            _importWorker.ProgressChanged += onImportWorkerOnProgressChanged;
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

                MessageBoxResult result =
                    MessageBox.Show(
                        LanguageDictionary.CurrentDictionary.Translate<string>("Messages.OutlookProblem", "Message"),
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

        public event EventHandler<ProgressChangedEventArgs> ImportProgressChanged;
        public event EventHandler<RunWorkerCompletedEventArgs> ImportProgressCompleted;

        public ObservableCollection<object> GetContactsFolders()
        {
            var folders = new ObservableCollection<object>();

            foreach (object folder in _contactsFolder.Folders)
            {
                folders.Add(folder);
            }
            return folders;
        }

        public ObservableCollection<object> GetContactsFromFolder(Folder folder = null)
        {
            Items contacts = folder == null ? _contactsFolder.Items : folder.Items;
            _contactsFromFolder = new ObservableCollection<object>();

            foreach (object contact in contacts)
            {
                _contactsFromFolder.Add(contact);
            }

            //var c = new ContactItem();
            //c.Email1Address

            return _contactsFromFolder;
        }

        public void StartImport(IEnumerable<object> contacts)
        {
            _importWorker.RunWorkerAsync(_contactsFromFolder);
        }

        private void onImportWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ImportProgressChanged != null)
                ImportProgressChanged.Invoke(this, e);
        }

        private void onImportWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ImportProgressCompleted != null)
                ImportProgressCompleted.Invoke(this, e);
        }

        private void onImportWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            var contacts = (ObservableCollection<object>) e.Argument;

            for (int i = 0; i < contacts.Count; i++)
            {
                ContactItem contactItem = (ContactItem)contacts[i];
                var contact = new Contact();
                contact.Title = contactItem.Title;
                contact.FirstName = contactItem.FirstName;
                contact.LastName = contactItem.LastName;
                contact.Suffix = contactItem.Suffix;
                contact.Telephone = contactItem.BusinessTelephoneNumber;
                contact.Fax = contactItem.BusinessFaxNumber;
                contact.Mobile = contactItem.MobileTelephoneNumber;
                contact.HomeTelephone = contactItem.HomeTelephoneNumber;
                contact.Website = contactItem.WebPage;
                contact.EmailAddress = contactItem.Email1Address;
                contact.EmailAddress1 = contactItem.Email2Address;
                contact.EmailAddress2 = contactItem.Email3Address;

                var company = CompaniesManager.Current.LoadCompanies().Where(c => c.Name == contactItem.CompanyName).FirstOrDefault();
                contact.Company = company;

                ContactsManager.Current.Save(contact);

                _importWorker.ReportProgress(Convert.ToInt32(Math.Round((double) (i+1)/contacts.Count*100.0, 0)));
            }
        }
    }
}