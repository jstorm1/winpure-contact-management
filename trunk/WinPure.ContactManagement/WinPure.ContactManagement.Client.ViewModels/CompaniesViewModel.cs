#region References

using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.CustomMessageBox;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.ViewModels.Base;
using WinPure.ContactManagement.Common.Helpers;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    /// <summary>
    /// View Model which manages companies.
    /// </summary>
    public class CompaniesViewModel : ViewModelBase
    {
        #region Fields

        private SynchronizedObservableCollection<Company> _companies;
        private int _contactsCount;
        private RelayCommand _deleteCommand;
        private SynchronizedObservableCollection<Company> _originalCompaniesCollection;
        private RelayCommand _searchCommand;
        private string _searchText;
        private Company _selectedCompany;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CompaniesViewModel()
        {
            //Check for the Design mode.
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

            //Load Companies List From database.
            Companies = CompaniesManager.Current.LoadCompanies();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Collection of Companies.
        /// </summary>
        public SynchronizedObservableCollection<Company> Companies
        {
            get { return _companies; }
            set
            {
                if (_companies == value) return;
                _companies = value;
                RaisePropertyChanged("Companies");
            }
        }

        /// <summary>
        /// Currently selected Company.
        /// </summary>
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                if (_selectedCompany == value) return;
                _selectedCompany = value;

                //Sets Contacts count.
                if (_selectedCompany != null)
                    ContactsCount = _selectedCompany.Contacts.Count;

                RaisePropertyChanged("SelectedCompany");
            }
        }

        /// <summary>
        /// Count of contacts which are related to the <see cref="SelectedCompany"/>.
        /// </summary>
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

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText == value) return;
                _searchText = value;
                RaisePropertyChanged("SearchText");
            }
        }

        #region Commands

        /// <summary>
        /// Search Company Command.
        /// </summary>
        public RelayCommand SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new RelayCommand(search)); }
        }

        /// <summary>
        /// Delete Company Command.
        /// </summary>
        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(delete)); }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Delete Selected Company.
        /// </summary>
        private void delete()
        {
            WPFMessageBoxResult result = WPFMessageBox.Show("Delete Company",
                                                            "Are you sure you want to delete this Company?",
                                                            WPFMessageBoxButtons.YesNo,
                                                            WPFMessageBoxImage.Question);
            if (result == WPFMessageBoxResult.No) return;

            CompaniesManager.Current.Delete(_selectedCompany);
        }

        private void search()
        {
            search(SearchText);
        }

        /// <summary>
        /// Search Company by name.
        /// </summary>
        /// <param name="companyName">Company Name</param>
        private void search(string companyName)
        {
            if (!string.IsNullOrEmpty(companyName))
            {
                if (_originalCompaniesCollection == null) _originalCompaniesCollection = Companies;

                Companies = CompaniesManager.Current.Search("Name", companyName);
            }
            else
            {
                if (_originalCompaniesCollection == null) return;
                Companies = _originalCompaniesCollection;
                _originalCompaniesCollection = null;
            }
        }

        #endregion
    }
}