#region References

using System.Collections.ObjectModel;
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
        private Company _selectedCompany;
        private int _contactsCount;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CompaniesViewModel()
        {
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

        #endregion
    }
}