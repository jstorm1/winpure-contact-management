using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.CSV
{
    public class ProgressScrenViewModel:ViewModelBase 
    {
        private bool _isSelected;
        private RelayCommand _startImportCommand;

        public RelayCommand StartImportCommand
        {
            get { return _startImportCommand ?? (_startImportCommand = new RelayCommand(startImport)); }
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
            startImport();
        }

        private static void startImport()
        {
            CsvImportManager.Current.StartImport();
        }
    }
}
