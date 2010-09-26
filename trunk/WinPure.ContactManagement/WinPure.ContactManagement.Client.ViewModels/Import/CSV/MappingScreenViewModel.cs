using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.CSV
{
    public class MappingScreenViewModel : ViewModelBase
    {

        private ObservableCollection<dynamic> _mapping;

        public MappingScreenViewModel()
        {
            if (IsDesignMode) return;

            Mapping = CsvImportManager.Current.GetDefaultMapping();
        }

        public ObservableCollection<object> Mapping
        {
            get { return _mapping; }
            set
            {
                if (_mapping == value) return;
                _mapping = value;
                RaisePropertyChanged("Mapping");
            }
        }
    }
}