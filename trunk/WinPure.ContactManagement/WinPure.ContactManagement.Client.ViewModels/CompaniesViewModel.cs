#region References

using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class CompaniesViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<Company> _companies;

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

        #endregion
    }
}