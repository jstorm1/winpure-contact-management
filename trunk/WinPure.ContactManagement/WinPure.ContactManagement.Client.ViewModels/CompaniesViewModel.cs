#region References

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.ViewModels.Base;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class CompaniesViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<Company> _companies;
        private int _contactsCount;
        private RelayCommand<string> _searchCommand;
        private Company _selectedCompany;
        private ObservableCollection<Company> _originalCompaniesCollection;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CompaniesViewModel()
        {
            //Check for Design mode.
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;
            
            Companies = CompaniesManager.Current.LoadCompanies();
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
                RaisePropertyChanged("Companies");
            }
        }

        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                if (_selectedCompany == value) return;
                _selectedCompany = value;

                if (_selectedCompany != null)
                    ContactsCount = _selectedCompany.Contacts.Count;

                RaisePropertyChanged("SelectedCompany");
            }
        }

        public int ContactsCount
        {
            get { return _contactsCount; }
            set
            {
                if (_contactsCount == value) return;
                _contactsCount = value;
                RaisePropertyChanged("ContactsCount");
            }
        }

        public RelayCommand<string> SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new RelayCommand<string>(search)); }
        }

        #endregion

        #region Methods
        
        private void search(string companyName)
        {
            if (companyName != "")
            {
                if (_originalCompaniesCollection == null) _originalCompaniesCollection = Companies;
                Companies = new ObservableCollection<Company>(Companies.Where(c => c.Name.ToUpper().Contains(companyName.ToUpper())));
            }
            else
            {
                Companies = _originalCompaniesCollection;
                _originalCompaniesCollection = null;
            }
        } 

        #endregion
    }
}