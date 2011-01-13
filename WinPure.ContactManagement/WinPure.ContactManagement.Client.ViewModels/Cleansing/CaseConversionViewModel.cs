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

            IsLowerCaseChecked = false;
            IsUpperCaseChecked = false;
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

        private RelayCommand _convertCaseCommand;
        public RelayCommand ConvertCaseCommand
        {
            get
            {
                if (_convertCaseCommand == null)
                    _convertCaseCommand = new RelayCommand(ConvertCaseAction);

                return _convertCaseCommand;
            }
        }

        private RelayCommand<SelectionChangedEventArgs> _columnsSelectionChangedCommand;
        public RelayCommand<SelectionChangedEventArgs> ColumnsSelectionChangedCommand
        {
            get
            {
                if (_columnsSelectionChangedCommand == null)
                    _columnsSelectionChangedCommand =
                        new RelayCommand<SelectionChangedEventArgs>(ColumnsSelectionChangedAction);

                return _columnsSelectionChangedCommand;
            }
        }

        #endregion

        #region Commands Actions

        private void ConvertCaseAction()
        {
            if (IsLowerCaseChecked == true)
                ConvertCellsToCase(Case.Lower);

            if (IsUpperCaseChecked == true)
                ConvertCellsToCase(Case.Upper);

            if (IsProperCaseChecked == true)
                ConvertCellsToCase(Case.Proper);
        }

        private void ColumnsSelectionChangedAction(SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                _selectedInColumns.Add(e.AddedItems[0] as string);
            else
                _selectedInColumns.Remove(e.RemovedItems[0] as string);
        }

        #endregion

        #region Helpers

        private void ConvertCellsToCase(Case conversionCase)
        {
            SynchronizedObservableCollection<Contact> contacts = ContactsManager.Current.ContactsCache;

            // If contacts is not empty
            if (contacts.Count > 0)
            {
                foreach (string column in _selectedInColumns)
                {
                    // In every contact
                    foreach (Contact contact in contacts)
                    {
                        PropertyInfo propertyInfo = contact.GetType().GetProperty(column);

                        // Get value of specified column
                        string fieldValue = propertyInfo.GetValue(contact, null) as string;

                        if (!string.IsNullOrEmpty(fieldValue))
                        {
                            string newValue = string.Empty;

                            switch (conversionCase)
                            {
                                case Case.Lower:

                                    newValue = fieldValue.ToLower();

                                    break;

                                case Case.Upper:

                                    newValue = fieldValue.ToUpper();

                                    break;

                                case Case.Proper:

                                    fieldValue = fieldValue.ToLower();
                                    newValue = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(fieldValue);

                                    break;
                            }

                            // Set new value
                            propertyInfo.SetValue(contact, newValue, null);
                        }
                    }
                }
            }

            ContactsManager.Current.Save(contacts);
        }

        #endregion
    }
}
