﻿#region References

using System;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Linq;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers.Base;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Data.Model.Extensions;
using WinPure.ContactManagement.Common.Helpers;

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers.DataManagers
{
    public class CompaniesManager : DataManagerBase
    {
        private SynchronizedObservableCollection<Company> _companiesCache;

        #region Singleton constructor

        private static CompaniesManager _instance;

        private CompaniesManager()
        {
            Services.SyncService.DatabaseChanged += onSyncServiceDatabaseChanged;
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
        public SynchronizedObservableCollection<Company> LoadCompanies()
        {
            RefreshCache();
            return _companiesCache;
        }

        /// <summary>
        /// Method which discards changes in Company, and loads Company state from database.
        /// </summary>
        /// <param name="company">Company which will be reverted.</param>
        public void Revert(Company company)
        {
            if (company == null) throw new ArgumentNullException("company");
            if (company.CompanyId == Guid.Empty ||
                Context.Companies.Where(c => c.CompanyId == company.CompanyId).FirstOrDefault() == null) return;

            Context.Refresh(RefreshMode.StoreWins, company);
        }

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        /// <param name="company"></param>
        public void Save(Company company)
        {
            if (company == null) throw new ArgumentNullException("company");
            if (company.CompanyId == Guid.Empty ||
                Context.Companies.Where(c => c.CompanyId == company.CompanyId).FirstOrDefault() == null)
            {
                Context.Companies.AddObject(company);
            }

            Context.SaveChanges();

            RefreshCache();
        }

        /// <summary>
        /// Method for deleting company from database.
        /// </summary>
        /// <param name="company">Company which will be deleted.</param>
        public void Delete(Company company)
        {
            if (company == null) throw new ArgumentNullException("company");

            if (company.CompanyId == Guid.Empty ||
                Context.Companies.Where(c => c.CompanyId == company.CompanyId).FirstOrDefault() == null) return;

            Context.DeleteObject(company);
            Context.SaveChanges();
            RefreshCache();
        }

        /// <summary>
        /// Method for refreshing local Companies cache.
        /// </summary>
        public void RefreshCache()
        {
            Context.Refresh(RefreshMode.StoreWins, Context.Companies);

            if (_companiesCache == null) _companiesCache = new SynchronizedObservableCollection<Company>(new ObservableCollection<Company>());

            _companiesCache.Clear();

            foreach (Company company in Context.Companies)
            {
                _companiesCache.Add(company);
            }
        }

        /// <summary>
        /// Method which will check company name in collection of already saved Companies, to find same. 
        /// I.e. Company name must be unique value, if not you will get false value as result.
        /// </summary>
        /// <param name="company">Company which will be checked.</param>
        /// <returns>
        /// false - Company name is not Unique.
        /// true - Company name is unique.
        /// </returns>
        public bool CheckCompanyNameForUnique(Company company)
        {
            return !company.IsCompanyNameUnique(Context);
        }

        private void onSyncServiceDatabaseChanged(object sender, EventArgs e)
        {
            RefreshCache();
        }
    }
}