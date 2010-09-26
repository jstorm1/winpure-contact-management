using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using LumenWorks.Framework.IO.Csv;
using WinPure.ContactManagement.Client.Data.Model;

namespace WinPure.ContactManagement.Client.Data.Managers.Import
{
    public class CsvImportManager
    {
        private string _currentFile;

        #region Singleton Construtor
        
        private static CsvImportManager _instance;
        private ObservableCollection<object> _defaultMapping;

        private CsvImportManager()
        {

        }

        public static CsvImportManager Current
        {
            get { return _instance ?? (_instance = new CsvImportManager()); }
        } 

        #endregion

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
                        ((IDictionary<string, object>)record).Add(headers[i], csv[i] ?? "");
                    }
                    records.Add(record);
                }

            }

            return records;
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


            _defaultMapping = new ObservableCollection<object>();
            var fileFields = GetColumns();

            var emptyContact = new Contact();
            var contactMembers = emptyContact.GetType().GetProperties();
            foreach (var contactMember in contactMembers)
            {
                if (hideProperties.Contains(contactMember.Name)) continue;

                dynamic field = new ExpandoObject();
                field.CrmField = contactMember.Name;
                field.FileField = "";
                field.FileFields = fileFields;

                _defaultMapping.Add(field);
            }
            return _defaultMapping;
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
