using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.Outlook
{
    public class OutlookImportProgressViewModel : ViewModelBase
    {
        private int _completedPercent;
        private bool _isImportCompleted;
        private ObservableCollection<object> _contacts;
        private RelayCommand _startImportCommand;

        public OutlookImportProgressViewModel()
        {
            //Check for Design mode.
            if (IsDesignMode) return;

            OutlookImportManager.Current.ImportProgressChanged += onOutlookManagerImportProgressChanged;
            OutlookImportManager.Current.ImportProgressCompleted += onOutlookImportManagerImportCompleted;
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

        public RelayCommand StartImportCommand
        {
            get { return _startImportCommand ?? (_startImportCommand = new RelayCommand(startImport)); }
        }

        public ObservableCollection<object> Contacts
        {
            get { return _contacts; }
            set
            {
                if (_contacts == value) return;
                _contacts = value;
                RaisePropertyChanged("Contacts");
            }
        }

        public bool IsImportCompleted
        {
            get { return _isImportCompleted; }
            private set
            {
                if (_isImportCompleted == value) return;
                _isImportCompleted = value;
                RaisePropertyChanged("IsImportCompleted");
            }
        }

        private void onOutlookImportManagerImportCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsImportCompleted = true;
        }

        private void onOutlookManagerImportProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CompletedPercent = e.ProgressPercentage;
        }

        private void startImport()
        {
            OutlookImportManager.Current.StartImport(Contacts);
        }
    }
}