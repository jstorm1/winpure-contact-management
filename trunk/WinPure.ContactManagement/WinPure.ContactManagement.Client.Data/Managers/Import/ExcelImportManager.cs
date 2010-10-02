using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Excel;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Client.Data.Model;

namespace WinPure.ContactManagement.Client.Data.Managers.Import
{
    /* ****************************************
     * Excel Data Reader - Read Excel files in .NET
     * http://exceldatareader.codeplex.com/
     * **************************************** */

    public class ExcelImportManager
    {
        public event EventHandler<ProgressChangedEventArgs> ImportProgressChanged;
        public event EventHandler<RunWorkerCompletedEventArgs> ImportProgressCompleted;

        private readonly BackgroundWorker _importWorker;
        private string _currentFile;
        private ObservableCollection<dynamic > _currentMapping;
        private string _currentTable;

        #region Singleton Constructor
        private static ExcelImportManager _instance;

        private ExcelImportManager()
        {
            _importWorker = new BackgroundWorker();
            _importWorker.WorkerReportsProgress = true;
            _importWorker.DoWork += onImportWorkerOnDoWork;
            _importWorker.RunWorkerCompleted += onImportWorkerOnRunWorkerCompleted;
            _importWorker.ProgressChanged += onImportWorkerOnProgressChanged;
        }

        public static ExcelImportManager Current
        {
            get { return _instance ?? (_instance = new ExcelImportManager()); }
        }

        #endregion

        public void SetCurrentFile(string path)
        {
            _currentFile = path;
        }

        public void StartImport()
        {
            _importWorker.RunWorkerAsync();
        }

        public void SetMapping(ObservableCollection<dynamic> mapping)
        {
            _currentMapping = mapping;
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
            var fields = _currentMapping.Where(f => !string.IsNullOrEmpty(f.FileField));
            if (fields.Count() == 0)
            {
                _importWorker.ReportProgress(100);
                return;
            }

            var records = GetRecords(_currentTable);

            for (int i = 0; i < records.Count; i++)
            {
                var record = records[i];
                var contact = new Contact();
                foreach (var field in fields)
                {
                    var value = (object)((IDictionary<string, object>) record)[field.FileField];
                    value = value is DBNull? "": value;

                    contact.GetType().GetProperty((string) field.CrmField).SetValue(contact,value, null);
                }

                ContactsManager.Current.Save(contact);

                _importWorker.ReportProgress(Convert.ToInt32(Math.Round((double) (i + 1)/records.Count*100.0, 0)));
            }
        }

        public ObservableCollection<dynamic> GetDefaultMapping()
        {
            _currentMapping = new ObservableCollection<object>();

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

            ObservableCollection<string> fileFields = GetColumns(_currentTable);

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

        public ObservableCollection<string> GetTables()
        {
            var tables = new ObservableCollection<string>();

            IExcelDataReader excelReader = getReader();
            var result = excelReader.AsDataSet();

            foreach (DataTable table in result.Tables)
            {
                tables.Add(table.TableName);
            }

            excelReader.Close();

            return tables;
        }

        public ObservableCollection<dynamic> GetRecords(string table)
        {
            _currentTable = table;
            var records = new ObservableCollection<dynamic>();

            IExcelDataReader excelReader = getReader();

            var result = excelReader.AsDataSet();
            foreach (DataRow row in result.Tables[table].Rows)
            {
                dynamic record = new ExpandoObject();
                foreach (DataColumn column in result.Tables[table].Columns)
                {
                    ((IDictionary<string, object>)record).Add(column.ColumnName, row[column]);
                }
                records.Add(record);
            }

            return records;
        }

        public ObservableCollection<string> GetColumns(string tableName)
        {
            var columns = new ObservableCollection<string>();

            IExcelDataReader excelReader = getReader();

            var result = excelReader.AsDataSet();

            var table = result.Tables[tableName];

            foreach (DataColumn column in table.Columns)
            {
                columns.Add(column.ColumnName);
            }

            excelReader.Close();
            return columns;
        }

        private IExcelDataReader getReader()
        {
            FileStream stream = File.Open(_currentFile, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader;

            var fileInfo = new FileInfo(_currentFile);
            switch (fileInfo.Extension)
            {
                case ".xlsx":
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    break;
                case ".xls":
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    break;

                default:
                    throw new NotSupportedException();
            }

            excelReader.IsFirstRowAsColumnNames = true;
            return excelReader;
        }
    }
}
