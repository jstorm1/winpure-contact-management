#region References

using System;
using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Model;
using System.Linq;

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers
{
    /// <summary>
    /// Class for managing operations between contact entities and database.
    /// </summary>
    public class ContactsManager : DataManagerBase
    {
        private ObservableCollection<Contact> _contactsCache;
        
        #region Singleton constructor

        private static ContactsManager _instance;

        private ContactsManager()
        {
        }

        /// <summary>
        /// Returns current instance.
        /// </summary>
        public static ContactsManager Current
        {
            get { return _instance ?? (_instance = new ContactsManager()); }
        }

        #endregion

        /// <summary>
        /// Method for loading contacts collection from database.
        /// </summary>
        /// <returns>Contacts collection</returns>
        public ObservableCollection<Contact> LoadContacts()
        {
            refreshCache();
            return _contactsCache;
        }

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        /// <param name="contact"></param>
        public void Save(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException("contact");
            if(contact.ContactID == Guid.Empty|| Context.Contacts.Where(c => c.ContactID == contact.ContactID).FirstOrDefault() == null)
            {
                if (contact.ContactID == Guid.Empty) contact.ContactID = Guid.NewGuid();
                Context.Contacts.AddObject(contact);
            }
            Context.SaveChanges();

            refreshCache();
        }

        private void refreshCache()
        {
            if (_contactsCache == null) _contactsCache = new ObservableCollection<Contact>();

            _contactsCache.Clear();

            foreach (var contact in Context.Contacts)
            {
                _contactsCache.Add(contact);
            }
        }
    }
}