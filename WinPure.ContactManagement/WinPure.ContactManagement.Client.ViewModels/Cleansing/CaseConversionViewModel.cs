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

        #endregion

        #region Constructor

        public CaseConversionViewModel()
        {
            Columns = new ObservableCollection<string>(
                ContactExtension.GetContactsColumnNames(new Contact()));
        }

        #endregion

        #region Properties

        public int SelectedIndex { get; set; }
        public ObservableCollection<string> Columns { get; set; }

        #endregion

        #region Commands

        private RelayCommand _convertToLowerCase;
        public RelayCommand ConvertToLowerCase
        {
            get
            {
                if (_convertToLowerCase == null)
                    _convertToLowerCase = new RelayCommand(ConvertToLowerCaseAction);

                return _convertToLowerCase;
            }
        }

        private RelayCommand _convertToUpperCase;
        public RelayCommand ConvertToUpperCase
        {
            get
            {
                if (_convertToUpperCase == null)
                    _convertToUpperCase = new RelayCommand(ConvertToUpperCaseAction);

                return _convertToUpperCase;
            }
        }

        private RelayCommand _convertToProperCase;
        public RelayCommand ConvertToProperCase
        {
            get
            {
                if (_convertToProperCase == null)
                    _convertToProperCase = new RelayCommand(ConvertToProperCaseAction);

                return _convertToProperCase;
            }
        }

        #endregion

        #region Commands Actions

        private void ConvertToLowerCaseAction()
        {
            ConvertCellsToCase(Case.Lower);
        }

        private void ConvertToUpperCaseAction()
        {
            ConvertCellsToCase(Case.Upper);
        }

        private void ConvertToProperCaseAction()
        {
            ConvertCellsToCase(Case.Proper);
        }

        #endregion

        #region Helpers

        private void ConvertCellsToCase(Case conversionCase)
        {
            SynchronizedObservableCollection<Contact> contacts = ContactsManager.Current.ContactsCache;

            // If contacts is not empty
            if (SelectedIndex >= 0)
            {
                // In every contact
                foreach (Contact contact in contacts)
                {
                    PropertyInfo propertyInfo = contact.GetType().GetProperty(Columns[SelectedIndex]);

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

            ContactsManager.Current.Save(contacts);
        }

        #endregion
    }
}
