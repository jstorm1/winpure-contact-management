using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Excel;

namespace WinPure.ContactManagement.Client.Data.Managers.Import
{
    /* ****************************************
     * Excel Data Reader - Read Excel files in .NET
     * http://exceldatareader.codeplex.com/
     * **************************************** */

    public class ExcelImportManager
    {
        private string _currentFile;

        #region Singleton Constructor
        private static ExcelImportManager _instance;

        private ExcelImportManager()
        {

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
