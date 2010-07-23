#region References

using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.ViewModels.Base;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class CompanyEditorViewModel : ViewModelBase
    {
        #region Fields
        
        private RelayCommand _cancelCommand;
        private Company _company;
        private RelayCommand _saveCommand; 

        #endregion

        #region Properties

        public bool IsCompanyNameUnique
        {
            get { return CompaniesManager.Current.CheckCompanyNameForUnique(_company); }
        }

        public Company Company
        {
            get { return _company; }
            set
            {
                if (_company == value) return;
                _company = value;
                RaisePropertyChanged("Company");

                _company.PropertyChanged += delegate { RaisePropertyChanged("IsCompanyNameUnique"); };
            }
        }

        #region Commands

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(save, canSave)); }
        }

        public RelayCommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new RelayCommand(cancel)); }
        }

        #endregion

        #endregion

        #region Methods

        private bool canSave()
        {
            if (IsDesignMode) return false;

            _company.Validate();
            return !_company.HasErrors;
        }

        private void cancel()
        {
            CompaniesManager.Current.Revert(_company);
        }

        private void save()
        {
            if (Company != null) CompaniesManager.Current.Save(Company);
        } 

        #endregion
    }
}