#region References

using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.ViewModels.Base;
using WinPure.ContactManagement.Common;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class ContactsViewModel : ViewModelBase
    {
        #region Fields

        private SynchronisedObservableCollection<Contact> _contacts;
        private Contact _selectedContact;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ContactsViewModel()
        {
            Contacts = ContactsManager.Current.LoadContacts();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Collection of Contacts.
        /// </summary>
        public SynchronisedObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                if (_contacts == value) return;
                _contacts = value;
                RaisePropertyChanged("Contacts");
            }
        }

        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                if (_selectedContact == value) return;
                _selectedContact = value;
                RaisePropertyChanged("SelectedContact");
            }
        }

        #endregion
    }
}