using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.CSV
{
    public class PreviewScreenViewModel : ViewModelBase
    {
        private ObservableCollection<string> _columns;
        private ObservableCollection<dynamic> _records;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PreviewScreenViewModel()
        {
            if (IsDesignMode) return;

            Columns = CsvImportManager.Current.GetColumns();
            Records = CsvImportManager.Current.GetRecords();
        }

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
    }
}