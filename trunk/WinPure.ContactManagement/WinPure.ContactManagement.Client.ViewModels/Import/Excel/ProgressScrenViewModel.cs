using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.Excel
{
    public class ProgressScrenViewModel : ViewModelBase
    {
        private int _completedPercent;
        private bool _isImportCompleted;
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

        public int CompletedPercent
        {
            get { return _completedPercent; }
            set
            {
                if (_completedPercent == value) return;
                _completedPercent = value;
                RaisePropertyChanged("CompletedPercent");
            }
        }

        public bool IsImportCompleted
        {
            get { return _isImportCompleted; }
            set
            {
                if (_isImportCompleted == value) return;
                _isImportCompleted = value;
                RaisePropertyChanged("IsImportCompleted");
            }
        }

        private void initialize()
        {
            ExcelImportManager.Current.ImportProgressChanged += onCsvImportManagerImportProgressChanged;
            ExcelImportManager.Current.ImportProgressCompleted += onCsvImportManagerImportProgressCompleted;

            startImport();
        }

        private void onCsvImportManagerImportProgressCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsImportCompleted = true;
        }

        private void onCsvImportManagerImportProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CompletedPercent = e.ProgressPercentage;
        }

        private static void startImport()
        {
            ExcelImportManager.Current.StartImport();
        }
    }
}