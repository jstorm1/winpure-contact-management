#region References

using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.ViewModels.Base;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class ContactEditorViewModel : ViewModelBase
    {
        #region Fields

        private RelayCommand _cancelCommand;
        private Contact _contact;
        private RelayCommand _saveCommand;

        #endregion

        #region Properties

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

        private void cancel()
        {
            ContactsManager.Revert(_contact);
        }

        private bool canSave()
        {
            if (IsDesignMode) return false;

            _contact.Validate();
            return !_contact.HasErrors;
        }

        private void save()
        {
            if (Contact != null) ContactsManager.Current.Save(Contact);
        }

        #endregion
    }
}