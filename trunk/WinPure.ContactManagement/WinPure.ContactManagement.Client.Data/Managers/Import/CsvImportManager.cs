using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using LumenWorks.Framework.IO.Csv;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Client.Data.Model;

namespace WinPure.ContactManagement.Client.Data.Managers.Import
{
    public class CsvImportManager
    {
        private readonly BackgroundWorker _importWorker;
        private string _currentFile;
        private ObservableCollection<dynamic > _currentMapping;

        #region Singleton Construtor

        private static CsvImportManager _instance;


        private CsvImportManager()
        {
            _importWorker = new BackgroundWorker();
            _importWorker.WorkerReportsProgress = true;
            _importWorker.DoWork += onImportWorkerOnDoWork;
            _importWorker.RunWorkerCompleted += onImportWorkerOnRunWorkerCompleted;
            _importWorker.ProgressChanged += onImportWorkerOnProgressChanged;
        }

        public static CsvImportManager Current
        {
            get { return _instance ?? (_instance = new CsvImportManager()); }
        }

        #endregion

        public event EventHandler<ProgressChangedEventArgs> ImportProgressChanged;
        public event EventHandler<RunWorkerCompletedEventArgs> ImportProgressCompleted;

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
            var fields = _currentMapping.Where(f => !string.IsNullOrEmpty(f.FileField));

            if(fields.Count() == 0)
            {
                _importWorker.ReportProgress(100);
                return;
            }

            var records = GetRecords();
            var contacts = new List<Contact>();

            for (int i = 0; i < records.Count; i++)
            {
                var record = records[i];
                var contact = new Contact();
                foreach (var field in fields)
                {
                    contact.GetType().GetProperty((string) field.CrmField).SetValue(contact,
                                                                                    (object)
                                                                                    ((IDictionary<string, object>)
                                                                                     record)[field.FileField], null);
                }
                //ContactsManager.Current.Save(contact);
                contacts.Add(contact);

                _importWorker.ReportProgress(Convert.ToInt32(Math.Round((double)(i + 1) / records.Count * 80.0, 0)));
            }

            ContactsManager.Current.Save(contacts);
            _importWorker.ReportProgress(100);
        }

        public void SetCurrentFile(string path)
        {
            _currentFile = path;
        }


        public ObservableCollection<dynamic> GetRecords()
        {
            var records = new ObservableCollection<dynamic>();

            // open the file "data.csv" which is a CSV file with headers
            using (var csv = new CsvReader(
                new StreamReader(_currentFile), true))
            {
                // missing fields will not throw an exception,
                // but will instead be treated as if there was a null value
                csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;

                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();

                while (csv.ReadNextRecord())
                {
                    dynamic record = new ExpandoObject();
                    for (int i = 0; i < fieldCount; i++)
                    {
                        ((IDictionary<string, object>) record).Add(headers[i], csv[i] ?? "");
                    }
                    records.Add(record);
                }
            }

            return records;
        }

        public void SetMapping(ObservableCollection<dynamic> mapping)
        {
            _currentMapping = mapping;
        }

        public ObservableCollection<dynamic> GetDefaultMapping()
        {
            var hideProperties = new List<string>
                                     {
                                         "ContactID",
                                         "CompanyId",
                                         "Company",
                                         "CompanyReference",
                                         "HasErrors",
                                         "Item",
                                         "Error",
                                         "EntityState",
                                         "EntityKey"
                                     };


            _currentMapping = new ObservableCollection<object>();
            ObservableCollection<string> fileFields = GetColumns();

            var emptyContact = new Contact();
            PropertyInfo[] contactMembers = emptyContact.GetType().GetProperties();
            foreach (PropertyInfo contactMember in contactMembers)
            {
                if (hideProperties.Contains(contactMember.Name)) continue;

                dynamic field = new ExpandoObject();
                field.CrmField = contactMember.Name;
                field.FileField = "";
                field.FileFields = fileFields;

                _currentMapping.Add(field);
            }
            return _currentMapping;
        }

        public void StartImport()
        {
            _importWorker.RunWorkerAsync();
        }

        public ObservableCollection<string> GetColumns()
        {
            string[] headers;

            // open the file "data.csv" which is a CSV file with headers
            using (var csv = new CsvReader(
                new StreamReader(_currentFile), true))
            {
                // missing fields will not throw an exception,
                // but will instead be treated as if there was a null value
                csv.MissingFieldAction = MissingFieldAction.ReplaceByNull;

                // to replace by "" instead, then use the following action:
                //csv.MissingFieldAction = MissingFieldAction.ReplaceByEmpty;
                headers = csv.GetFieldHeaders();
            }

            return new ObservableCollection<string>(headers);
        }
    }
}