#region References

using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class CompaniesViewModel
    {
        #region Fields

        private RelayCommand _command;
        private ObservableCollection<Company> _companies;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CompaniesViewModel()
        {
            _command = new RelayCommand(test);
            Companies = CompaniesManager.Current.LoadCompanies();
        }

        private void test()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Collection of Companies.
        /// </summary>
        public ObservableCollection<Company> Companies
        {
            get { return _companies; }
            set
            {
                if (_companies == value) return;
                _companies = value;
                //RaisePropertyChanged("Companies");
            }
        }

        #endregion
    }
}