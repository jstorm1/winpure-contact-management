#region References

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Data.SqlServerCe;
using System.Linq;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers.Base;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Common;
using WinPure.ContactManagement.Common.Helpers;

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers.DataManagers
{
    /// <summary>
    /// Class for managing operations between contact entities and database.
    /// </summary>
    public class ContactsManager : DataManagerBase
    {
        private SynchronizedObservableCollection<Contact> _contactsCache;

        #region Singleton constructor

        private static ContactsManager _instance;
        private string _orderByField = "LastName";

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

        public string OrderByField
        {
            get { return _orderByField; }
        }

        private void onSyncServiceDatabaseChanged(object sender, EventArgs e)
        {
            RefreshCache(OrderByField);
        }

        public SynchronizedObservableCollection<Contact> SearchByFullName(string pattern)
        {
            const string fields = "[Title] + ' ' + [FirstName] + ' ' + [LastName] + ' ' + [Suffix]";
            return Search(fields, pattern,
                          "SELECT *, {1} As FullName FROM {0} WHERE {1} LIKE @pattern OR {1} LIKE @filter OR {1}=@pattern");
        }

        public SynchronizedObservableCollection<Contact> Search(string propertyOrFieldName, string pattern,
                                                                string query = null)
        {
            query = string.Format(string.IsNullOrEmpty(query) ? Constants.SEARCH_QUERY_PATTERN : query, "Contact",
                                  propertyOrFieldName);

            ObjectResult<Contact> result = Context.ExecuteStoreQuery<Contact>(query,
                                                                              new SqlCeParameter
                                                                                  {
                                                                                      ParameterName = "pattern",
                                                                                      Value = pattern
                                                                                  },
                                                                              new SqlCeParameter
                                                                                  {
                                                                                      ParameterName = "filter",
                                                                                      Value = "%" + pattern + "%"
                                                                                  });
            var resultCollection = new ObservableCollection<Contact>(result);
            return new SynchronizedObservableCollection<Contact>(resultCollection);
        }

        /// <summary>
        /// Method for loading contacts collection from database.
        /// </summary>
        /// <returns>Contacts collection</returns>
        public SynchronizedObservableCollection<Contact> LoadContacts()
        {
            RefreshCache(OrderByField);
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

            RefreshCache(OrderByField);
        }

        /// <summary>
        /// Method which discards changes in Contact, and loads Contact state from database.
        /// </summary>
        /// <param name="contact">Contact which will be reverted.</param>
        public void Revert(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException("contact");
            if (contact.CompanyId == Guid.Empty ||
                Context.Contacts.Where(c => c.ContactID == contact.ContactID).FirstOrDefault() == null) return;

            Context.Refresh(RefreshMode.StoreWins, contact);
        }

        /// <summary>
        /// Removes contact from database.
        /// </summary>
        /// <param name="contact">Contact which will be removed.</param>
        public void Delete(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException("contact");
            if (contact.ContactID == Guid.Empty ||
                Context.Contacts.Where(c => c.ContactID == contact.ContactID).FirstOrDefault() == null) return;

            Context.DeleteObject(contact);
            Context.SaveChanges();
            RefreshCache(OrderByField);
        }

        private static IEnumerable<Contact> orderContactsByField(string fieldName)
        {
            if (fieldName == null) return Context.Contacts;
            return
                Context.Contacts.AsQueryable().Provider.CreateQuery<Contact>(Context.Contacts.OrderByProperty(
                    fieldName, null));
        }

        public void RefreshCache(string fieldName = null)
        {
            _orderByField = fieldName;
            Context.Refresh(RefreshMode.StoreWins, Context.Contacts);

            if (_contactsCache == null)
                _contactsCache = new SynchronizedObservableCollection<Contact>(new ObservableCollection<Contact>());

            _contactsCache.Clear();

            foreach (Contact contact in orderContactsByField(fieldName))
            {
                _contactsCache.Add(contact);
            }
        }
    }
}