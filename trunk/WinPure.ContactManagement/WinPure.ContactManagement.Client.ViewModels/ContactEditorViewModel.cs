#region References

using System;
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
        private RelayCommand _cancelCommand;

        #endregion

        #region Properties

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(save, canSave)); }
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

        public RelayCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new RelayCommand(cancel);
                return _cancelCommand;
            }
        }

        #endregion

        #region Methods

        private void cancel()
        {
            ContactsManager.Current.Revert(_contact);
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