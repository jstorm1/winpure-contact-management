#region References

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.CustomMessageBox;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.ViewModels.Base;
using WinPure.ContactManagement.Common;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class CompaniesViewModel : ViewModelBase
    {
        #region Fields

        private SynchronisedObservableCollection<Company> _companies;
        private RelayCommand _deleteCommand;
        private int _contactsCount;
        private RelayCommand<string> _searchCommand;
        private Company _selectedCompany;
        private SynchronisedObservableCollection<Company> _originalCompaniesCollection;

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
        public SynchronisedObservableCollection<Company> Companies
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

        public RelayCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(delete);
                return _deleteCommand;
            }
        }

        #endregion

        #region Methods

        private void delete()
        {
            WPFMessageBoxResult result = WPFMessageBox.Show("Delete Company",
                                                            "Are you sure you want to delete this Company?",
                                                            WPFMessageBoxButtons.YesNo,
                                                            WPFMessageBoxImage.Question);
            if (result == WPFMessageBoxResult.No) return;

            CompaniesManager.Current.Delete(_selectedCompany);
        }

        private void search(string companyName)
        {
            if (companyName != "")
            {
                if (_originalCompaniesCollection == null) _originalCompaniesCollection = Companies;
                Companies = new SynchronisedObservableCollection<Company>(new ObservableCollection<Company>( Companies.Where(c => c.Name.ToUpper().Contains(companyName.ToUpper()))));
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