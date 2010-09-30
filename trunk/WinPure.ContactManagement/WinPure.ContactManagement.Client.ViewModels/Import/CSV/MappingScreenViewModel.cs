using System;
using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.CSV
{
    public class MappingScreenViewModel : ViewModelBase
    {
        private bool _isSelected;
        private ObservableCollection<dynamic> _mapping;

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

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
                
                if(IsSelected) 
                    initialize(); 
                else 
                    CsvImportManager.Current.SetMapping(Mapping);
            }
        }

        private void initialize()
        {
            if (IsDesignMode) return;
            Mapping = CsvImportManager.Current.GetDefaultMapping();
        }
    }
}