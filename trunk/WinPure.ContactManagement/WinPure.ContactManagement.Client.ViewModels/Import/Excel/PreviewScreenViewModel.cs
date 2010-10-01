using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.Excel
{
    public class PreviewScreenViewModel : ViewModelBase
    {
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

        private void initialize()
        {
            if (IsDesignMode) return;

            //Columns = ExcelImportManager.Current.GetColumns();
            //Records = ExcelImportManager.Current.GetRecords();
        }
    }
}