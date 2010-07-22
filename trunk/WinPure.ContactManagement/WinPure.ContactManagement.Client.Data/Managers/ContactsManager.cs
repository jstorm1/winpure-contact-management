#region References

using System;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Linq;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Common;

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers
{
    /// <summary>
    /// Class for managing operations between contact entities and database.
    /// </summary>
    public class ContactsManager : DataManagerBase
    {
        private SynchronisedObservableCollection<Contact> _contactsCache;

        #region Singleton constructor

        private static ContactsManager _instance;

        private ContactsManager()
        {
            Services.SyncService.DatabaseChanged += onSyncServiceDatabaseChanged;
        }

        /// <summary>
        /// Returns current instance.
        /// </summary>
        public static ContactsManager Current
        {
            get { return _instance ?? (_instance = new ContactsManager()); }
        }

        #endregion

        private void onSyncServiceDatabaseChanged(object sender, EventArgs e)
        {
            RefreshCache();
        }

        /// <summary>
        /// Method for loading contacts collection from database.
        /// </summary>
        /// <returns>Contacts collection</returns>
        public SynchronisedObservableCollection<Contact> LoadContacts()
        {
            RefreshCache();
            return _contactsCache;
        }

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        /// <param name="contact"></param>
        public void Save(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException("contact");
            if (contact.ContactID == Guid.Empty ||
                Context.Contacts.Where(c => c.ContactID == contact.ContactID).FirstOrDefault() == null)
            {
                if (contact.ContactID == Guid.Empty) contact.ContactID = Guid.NewGuid();
                Context.Contacts.AddObject(contact);
            }
            Context.SaveChanges();

            RefreshCache();
        }

        /// <summary>
        /// Method which discards changes in Contact, and loads Contact state from database.
        /// </summary>
        /// <param name="contact">Contact which will be reverted.</param>
        public void Revert(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException("contact");
            if (contact.CompanyId == Guid.Empty || Context.Contacts.Where(c => c.ContactID == contact.ContactID).FirstOrDefault() == null) return;

            Context.Refresh(RefreshMode.StoreWins, contact);
        }

        public void RefreshCache()
        {
            Context.Refresh(RefreshMode.StoreWins, Context.Contacts);

            if (_contactsCache == null)
                _contactsCache = new SynchronisedObservableCollection<Contact>(new ObservableCollection<Contact>());

            _contactsCache.Clear();

            foreach (Contact contact in Context.Contacts)
            {
                _contactsCache.Add(contact);
            }
        }
    }
}