using System;
using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.CSV
{
    public class PreviewScreenViewModel : ViewModelBase
    {
        private ObservableCollection<string> _columns;
        private ObservableCollection<dynamic> _records;
        private bool _isSelected;

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

        private void initialize()
        {
            if (IsDesignMode) return;

            Columns = CsvImportManager.Current.GetColumns();
            Records = CsvImportManager.Current.GetRecords();
        }
    }
}