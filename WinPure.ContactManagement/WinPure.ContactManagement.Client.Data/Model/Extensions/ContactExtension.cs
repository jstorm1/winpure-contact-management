using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Common.Helpers;

namespace WinPure.ContactManagement.Client.Data.Model.Extensions
{
    public static class ContactExtension
    {
        /// <summary>
        /// Get column names of contacts
        /// </summary>
        /// <returns>List on column names</returns>
        public static IEnumerable<string> GetContactsColumnNames(this Contact contact)
        {
            // Not shown columns
            var hideProperties = new List<string>
                                     {
                                         "ContactID",
                                         "CompanyId",
                                         "Company",
                                         "CompanyReference",
                                         "HasErrors",
                                         "Item",
                                         "Error",
                                         "EntityState",
                                         "EntityKey"
                                     };

            PropertyInfo[] properties = typeof(Contact).GetProperties();

            return from property in properties where !hideProperties.Contains(property.Name) select property.Name;
        }

        /// <summary>
        /// Returns DataTable of Contact from contacts cache
        /// </summary>
        /// <returns>DataTable of Contact</returns>
        public static DataTable GetDataTableFromContacts()
        {
            List<string> columns = ContactExtension.GetContactsColumnNames(new Contact()).ToList();
            DataTable table = new DataTable();

            foreach (string column in columns)
            {
                table.Columns.Add(column);
            }

            SynchronizedObservableCollection<Contact> contacts = ContactsManager.Current.ContactsCache;

            DataRow row;
            string fieldValue;
            foreach (Contact contact in contacts)
            {
                row = table.NewRow();
                for (int i = 0; i < columns.Count; i++)
                {
                    // Get value of specified column
                    fieldValue = contact.GetType().GetProperty(columns[i]).GetValue(contact, null) as string;
                    row[i] = fieldValue;
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
