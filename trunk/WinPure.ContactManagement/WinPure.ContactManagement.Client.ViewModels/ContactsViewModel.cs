#region References

using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class ContactsViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<Contact> _contacts;

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
        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            set
            {
                if (_contacts == value) return;
                _contacts = value;
                RaisePropertyChanged("Contacts");
            }
        }

        #endregion
    }
}