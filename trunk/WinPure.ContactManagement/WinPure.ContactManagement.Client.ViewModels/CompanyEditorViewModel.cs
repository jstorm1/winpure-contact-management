#region References

using System;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.ViewModels.Base;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class CompanyEditorViewModel : ViewModelBase
    {
        private Company _company;
        private RelayCommand _saveCommand;
        private RelayCommand _cancelCommand;

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(save, canSave)); }
        }

        private bool canSave()
        {
            if (IsDesignMode) return false;

            _company.Validate();
            return !_company.HasErrors;
        }

        public Company Company
        {
            get { return _company; }
            set
            {
                if (_company == value) return;
                _company = value;
                RaisePropertyChanged("Company");
            }
        }

        public RelayCommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new RelayCommand(cancel)); }
        }

        private void cancel()
        {
            CompaniesManager.Current.Revert(_company);
        }

        private void save()
        {
            if (Company != null) CompaniesManager.Current.Save(Company);
        }
    }
}