#region References

using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Managers.Base;
using WinPure.ContactManagement.Client.Data.Model;

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers
{
    /// <summary>
    /// Class for managing operations between contact entities and database.
    /// </summary>
    public class ContactsManager : DataManagerBase
    {
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
            return new ObservableCollection<Contact>(Context.Contacts);
        }
    }
}