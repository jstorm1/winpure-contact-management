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
using System.Data;
using WinPure.ContactManagement.Client.Localization;
using System.Threading;

namespace WinPure.ContactManagement.Client.ViewModels.Cleansing
{
    public enum Case { Lower, Upper, Proper };

    public class CaseConversionViewModel : Base.ViewModelBase
    {
        #region Fields

        private string _distinctValues;
        private bool _isBusy;
        private string _busyMessage;
        private bool _isLowerCaseButtonEnabled = true;
        private bool _isUpperCaseButtonEnabled = true;
        private bool _isProperCaseButtonEnabled = true;
        private int _selectedIndex = -1;
		private int _distinctValuesHeight;

        #endregion

        #region Constructor

        public CaseConversionViewModel()
        {
            Columns = new ObservableCollection<string>(
                ContactExtension.GetContactsColumnNames(new Contact()));
        }

        #endregion

        #region Properties

        public int SelectedIndex 
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
            }
        }

        public ObservableCollection<string> Columns { get; set; }

        public DataTable AllValuesDataTable { get; set; }

        public string DistinctValues
        {
            get { return _distinctValues; }
            set
            {
                _distinctValues = value;
                RaisePropertyChanged("DistinctValues");
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        public string BusyMessage
        {
            get { return _busyMessage; }
            set
            {
                if (_busyMessage == value) return;
                _busyMessage = value;
                RaisePropertyChanged("BusyMessage");
            }
        }

        public bool IsLowerCaseButtonEnabled
        {
            get { return _isLowerCaseButtonEnabled; }
            set
            {
                _isLowerCaseButtonEnabled = value;
                RaisePropertyChanged("IsLowerCaseButtonEnabled");
            }
        }

        public bool IsUpperCaseButtonEnabled
        {
            get { return _isUpperCaseButtonEnabled; }
            set
            {
                _isUpperCaseButtonEnabled = value;
                RaisePropertyChanged("IsUpperCaseButtonEnabled");
            }
        }

        public bool IsProperCaseButtonEnabled
        {
            get { return _isProperCaseButtonEnabled; }
            set
            {
                _isProperCaseButtonEnabled = value;
                RaisePropertyChanged("IsProperCaseButtonEnabled");
            }
        }
		
		public int DistinctValuesHeight
		{
			get { return _distinctValuesHeight; }
			set
			{
				_distinctValuesHeight = value;
				RaisePropertyChanged("DistinctValuesHeight");
			}
		}

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

        private RelayCommand _columnSelectionChanged;
        public RelayCommand ColumnSelectionChanged
        {
            get
            {
                if (_columnSelectionChanged == null)
                    _columnSelectionChanged = new RelayCommand(ColumnSelectionChangedAction);

                return _columnSelectionChanged;
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

        private void ColumnSelectionChangedAction()
        {
            AllValuesDataTable = new DataTable();

            AllValuesDataTable.Columns.Add(Columns[SelectedIndex]);
            AllValuesDataTable.Columns.Add("Count");

            SynchronizedObservableCollection<Contact> contacts = ContactsManager.Current.ContactsCache;
            List<string> values = new List<string>(contacts.Count);

            // In every contact
            foreach (Contact contact in contacts)
            {
                PropertyInfo propertyInfo = contact.GetType().GetProperty(Columns[SelectedIndex]);

                // Get value of specified column
                string fieldValue = propertyInfo.GetValue(contact, null) as string;

                values.Add(fieldValue);
            }

            IEnumerable<string> distinctValues = values.Distinct();
            int distinctCount = 0;

            int nullCount = values.Count((v) => string.IsNullOrEmpty(v));
            if (nullCount > 0)
                AllValuesDataTable.Rows.Add(new string[] { "(null)", nullCount.ToString() });

            foreach (string value in distinctValues)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    AllValuesDataTable.Rows.Add(new string[] { value, values.Count((v) => v == value).ToString() });
                    distinctCount++;
                }
            }

            RaisePropertyChanged("AllValuesDataTable");

            DistinctValues = "DistinctValues:  " + distinctCount;
			DistinctValuesHeight = 22;
        }

        #endregion

        #region Helpers

        private void ConvertCellsToCase(Case conversionCase)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(CaseConvertionThreadProc));
            thread.IsBackground = true;
            thread.Start(conversionCase);
        }

        #endregion

        private void CaseConvertionThreadProc(object conversionCase)
        {
            int selectedIndex = SelectedIndex;
            IsBusy = true;
            BusyMessage = LanguageDictionary.CurrentDictionary.Translate<string>("Messages.BusyMessage.Working", "Message");
            IsLowerCaseButtonEnabled = false;
            IsUpperCaseButtonEnabled = false;
            IsProperCaseButtonEnabled = false;
            SynchronizedObservableCollection<Contact> contacts = ContactsManager.Current.ContactsCache;

            // If contacts is not empty
            if (selectedIndex >= 0)
            {
                // In every contact
                foreach (Contact contact in contacts)
                {
                    PropertyInfo propertyInfo = contact.GetType().GetProperty(Columns[selectedIndex]);

                    // Get value of specified column
                    string fieldValue = propertyInfo.GetValue(contact, null) as string;

                    if (!string.IsNullOrEmpty(fieldValue))
                    {
                        string newValue = string.Empty;

                        switch ((Case)conversionCase)
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
            ColumnSelectionChangedAction();
            IsBusy = false;
            IsLowerCaseButtonEnabled = true;
            IsUpperCaseButtonEnabled = true;
            IsProperCaseButtonEnabled = true;
        }
    }
}
