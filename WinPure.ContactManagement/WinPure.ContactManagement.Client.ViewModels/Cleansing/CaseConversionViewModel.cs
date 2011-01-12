using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Model.Extensions;
using WinPure.ContactManagement.Client.Data.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using WinPure.ContactManagement.Common.Helpers;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using System.Reflection;

namespace WinPure.ContactManagement.Client.ViewModels.Cleansing
{
    public enum Case { Lower, Upper, Proper };

    public class CaseConversionViewModel : Base.ViewModelBase
    {
#region Fields

        /// <summary>
        /// Selected values in 'Columns' list box
        /// </summary>
        private IList<string> _selectedInColumns = new List<string>();

#endregion

#region Constructor

        public CaseConversionViewModel()
        {
            Columns = new ObservableCollection<string>(
                ContactExtension.GetContactsColumnNames(new Contact()));

            IsLowerCaseChecked  = false;
            IsUpperCaseChecked  = false;
            IsProperCaseChecked = false;
        }

#endregion

#region Properties

        public bool? IsLowerCaseChecked { get; set; }
        public bool? IsUpperCaseChecked { get; set; }
        public bool? IsProperCaseChecked { get; set; }

        public ObservableCollection<string> Columns { get; set; }

#endregion

#region Commands


#endregion

#region Commands Actions


#endregion

#region Helpers


#endregion
    }
}
