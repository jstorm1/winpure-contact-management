using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.Excel
{
    public class PreviewScreenViewModel : ViewModelBase
    {
        private ObservableCollection<string> _tables;
        private string _selectedTable;
        private ObservableCollection<string> _columns;
        private bool _isSelected;
        private ObservableCollection<dynamic> _records;

        public ObservableCollection<string> Columns
        {
            get { return _columns; }
            set
            {
                if (_columns == value) return;
                _columns = value;
                RaisePropertyChanged("Columns");
            }
        }

        public ObservableCollection<object> Records
        {
            get { return _records; }
            set
            {
                if (_records == value) return;
                _records = value;
                RaisePropertyChanged("Records");
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                RaisePropertyChanged("IsSelected");

                if (IsSelected) initialize();
            }
        }

        public ObservableCollection<string> Tables
        {
            get { return _tables; }
            set
            {
                if (_tables == value) return;
                _tables = value;
                RaisePropertyChanged("Tables");
            }
        }

        public string SelectedTable
        {
            get { return _selectedTable; }
            set
            {
                if (_selectedTable == value) return;
                _selectedTable = value;

                Columns = ExcelImportManager.Current.GetColumns(value);
                Records = ExcelImportManager.Current.GetRecords(value);

                RaisePropertyChanged("SelectedTable");
            }
        }

        private void initialize()
        {
            if (IsDesignMode) return;

            Tables = ExcelImportManager.Current.GetTables();
            //Columns = ExcelImportManager.Current.GetColumns();
            //Records = ExcelImportManager.Current.GetRecords();
        }
    }
}