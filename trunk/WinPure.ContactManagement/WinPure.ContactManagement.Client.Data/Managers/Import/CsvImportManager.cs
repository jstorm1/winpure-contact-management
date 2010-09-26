using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinPure.ContactManagement.Client.Data.Managers.Import
{
    public class CsvImportManager
    {
        private string _currentFile;

        #region Singleton Construtor
        
        private static CsvImportManager _instance;

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
    }
}
