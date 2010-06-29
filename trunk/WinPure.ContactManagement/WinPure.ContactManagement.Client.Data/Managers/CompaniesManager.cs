#region References

using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Managers.Base;
using WinPure.ContactManagement.Client.Data.Model;

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers
{
    public class CompaniesManager : DataManagerBase
    {
        #region Singleton constructor

        private static CompaniesManager _instance;

        private CompaniesManager()
        {
        }

        public static CompaniesManager Current
        {
            get { return _instance ?? (_instance = new CompaniesManager()); }
        }

        #endregion

        /// <summary>
        /// Method for loading companies collection from database.
        /// </summary>
        /// <returns>Companies Collection</returns>
        public ObservableCollection<Company> LoadCompanies()
        {
            return new ObservableCollection<Company>(Context.Companies);
        }
    }
}