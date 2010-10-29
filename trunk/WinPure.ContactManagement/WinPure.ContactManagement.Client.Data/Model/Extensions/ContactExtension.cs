using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

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
    }
}
