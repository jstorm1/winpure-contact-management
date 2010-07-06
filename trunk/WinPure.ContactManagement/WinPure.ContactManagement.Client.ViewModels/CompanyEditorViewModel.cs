#region References

using System;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.ViewModels.Base;
using WinPure.ContactManagement.Common.Interfaces.Entities;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class CompanyEditorViewModel : ViewModelBase
    {
        private RelayCommand _saveCommand;
        private Company _company;

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save)); }
        }

        public Company Company
        {
            get { return _company; }
            set
            {
                if (_company == null)
                _company = value;
                RaisePropertyChanged("Company");
            }
        }

        private void Save()
        {
            CompaniesManager.Current.Save(Company);
        }
    }
}
