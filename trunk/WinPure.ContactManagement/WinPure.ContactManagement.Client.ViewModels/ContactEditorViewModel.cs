#region References

using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.ViewModels.Base;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class ContactEditorViewModel : ViewModelBase
    {
        #region Fields
        private Contact _contact;
        private RelayCommand _saveCommand;
        #endregion

        #region Properties
        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save)); }
        }

        public Contact Contact
        {
            get { return _contact; }
            set
            {
                if (_contact == value) return;
                _contact = value;
                RaisePropertyChanged("Contact");
            }
        }

        #endregion

        #region Methods
        public void Save()
        {
            if (Contact != null) ContactsManager.Current.Save(Contact);
        } 
        #endregion
    }
}