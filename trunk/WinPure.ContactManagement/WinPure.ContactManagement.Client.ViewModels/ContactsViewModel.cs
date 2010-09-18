#region References

using System;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.CustomMessageBox;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Localization;
using WinPure.ContactManagement.Client.ViewModels.Base;
using WinPure.ContactManagement.Common.Helpers;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class ContactsViewModel : ViewModelBase
    {
        #region Fields

        private SynchronizedObservableCollection<Contact> _contacts;
        private RelayCommand _deleteCommand;
        private Contact _selectedContact;
        private string _searchText;
        private SynchronizedObservableCollection<Contact> _originalContactsCollection;
        private RelayCommand _searchCommand;
        private string _sortByField;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ContactsViewModel()
        {
            Contacts = ContactsManager.Current.LoadContacts();

            SortByField = ContactsManager.Current.OrderByField;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Collection of Contacts.
        /// </summary>
        public SynchronizedObservableCollection<Contact> Contacts
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

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(delete)); }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText == value) return;
                _searchText = value;
                RaisePropertyChanged("SearchText");
            }
        }

        public RelayCommand SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new RelayCommand(search)); }
        }

        public string SortByField
        {
            set
            {
                _sortByField = value;
                changeContactsOrder(value);
            }
        }

        #endregion

        #region Methods

        private void changeContactsOrder(string fieldname)
        {
            ContactsManager.Current.RefreshCache(fieldname);
        }

        private void search()
        {
            search(SearchText);
        }

        private void search(string contactName)
        {
            if (!string.IsNullOrEmpty(contactName))
            {
                if (_originalContactsCollection == null) _originalContactsCollection = Contacts;

                Contacts = ContactsManager.Current.SearchByFullName(contactName);
            }
            else
            {
                if (_originalContactsCollection == null) return;
                Contacts = _originalContactsCollection;
                _originalContactsCollection = null;
            }
        }

        private void delete()
        {
            WPFMessageBoxResult result = WPFMessageBox.Show(LanguageDictionary.CurrentDictionary.Translate<string>("Messages.DeleteContact", "Title"),
                                                            LanguageDictionary.CurrentDictionary.Translate<string>("Messages.DeleteContact", "Message"),
                                                            WPFMessageBoxButtons.YesNo,
                                                            WPFMessageBoxImage.Question);
            if (result == WPFMessageBoxResult.No) return;

            ContactsManager.Current.Delete(_selectedContact);
        }

        #endregion
    }
}