﻿#region References

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Objects;
using System.Linq;
using WinPure.ContactManagement.Client.Data.Managers.Base;
using WinPure.ContactManagement.Client.Data.Model;

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers
{
    public class CompaniesManager : DataManagerBase
    {
        private ObservableCollection<Company> _companiesCache;

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
            refreshCache();
            return _companiesCache;
        }

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        /// <param name="company"></param>
        public void Save(Company company)
        {
            if (company.CompanyId == Guid.Empty || Context.Companies.Where(c => c.CompanyId == company.CompanyId).FirstOrDefault() == null)
            {
                Context.Companies.AddObject(company);
            }

            Context.SaveChanges();

            refreshCache();
        }

        private void refreshCache()
        {
            if (_companiesCache == null) _companiesCache = new ObservableCollection<Company>();

            _companiesCache.Clear();

            foreach (var company in Context.Companies)
            {
                _companiesCache.Add(company);
            }
        }

    }
}