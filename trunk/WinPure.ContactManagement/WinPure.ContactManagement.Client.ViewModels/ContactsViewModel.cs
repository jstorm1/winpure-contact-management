﻿#region References

using System;
using BlogsPrajeesh.BlogSpot.WPFControls;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.CustomMessageBox;
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
        private RelayCommand _deleteCommand;
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

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(delete)); }
        }

        #endregion

        #region Methods

        private void delete()
        {
            WPFMessageBox.Show("Delete Contact", "Are you sure you want to delete this Contact?",WPFMessageBoxButtons.YesNo,
                               WPFMessageBoxImage.Question);
        }

        #endregion
    }
}