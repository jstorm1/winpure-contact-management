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
        private Company _company;
        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(save)); }
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

        private void save()
        {
            if (Company != null) CompaniesManager.Current.Save(Company);
        }
    }
}