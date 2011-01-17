using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Model.Extensions;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.DeduplicationModule;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using System.Windows;

namespace WinPure.ContactManagement.Client.ViewModels.Cleansing
{
    public class DeduplicationViewModel : Base.ViewModelBase
    {
        #region Fields

        /// <summary>
        /// Selected values in 'Columns' list box
        /// </summary>
        private IList<string> _selectedInColumns = new List<string>();

        /// <summary>
        /// Selected values in 'Selected Columns' list box
        /// </summary>
        private IList<string> _selectedInSelectedColumns = new List<string>();

        /// <summary>
        /// Fuzzy threshold level
        /// </summary>
        private int _thresholdLevel = 90;

        private Color      _lastColor = Colors.LightBlue;        
        private string     _lastId;        
        private string     _totalDuplicates;
        private DataTable  _dedupTable;
        private Visibility _progressBarVisibility = Visibility.Collapsed;
        private int        _percentsDone;

        #endregion

        #region Constructor

        public DeduplicationViewModel()
        {
            Columns = new ObservableCollection<string>();
            SelectedColumns = new ObservableCollection<string>(ContactExtension.GetContactsColumnNames(new Contact()));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Data for 'Columns' list box
        /// </summary>
        public ObservableCollection<string> Columns { get; set; }

        /// <summary>
        /// Data for 'Selected Columns' list box
        /// </summary>
        public ObservableCollection<string> SelectedColumns { get; set; }

        /// <summary>
        /// Fuzzy threshold level
        /// </summary>
        public int ThresholdLevel
        {
            get { return _thresholdLevel; }
            set 
            { 
                _thresholdLevel = value;
                RaisePropertyChanged("ThresholdLevel");
            }
        }

        public DataTable DedupTable
        {
            get { return _dedupTable; }
            set
            {
                _dedupTable = value;
                RaisePropertyChanged("DedupTable");
            }
        }

        public string TotalDuplicates
        {
            get { return _totalDuplicates; }
            set
            {
                _totalDuplicates = value;
                RaisePropertyChanged("TotalDuplicates");
            }
        }

        public int DataSelectedIndex { get; set; }

        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set
            {
                _progressBarVisibility = value;
                RaisePropertyChanged("ProgressBarVisibility");
            }
        }

        public int PercentsDone
        {
            get { return _percentsDone; }
            set
            {
                _percentsDone = value;
                RaisePropertyChanged("PercentsDone");
            }
        }

        #endregion

        #region Commands

        private RelayCommand _moveAllFromSelectedColumnsToColumns;
        /// <summary>
        /// Move all values from 'Selected Columns' list box to 'Columns' list box
        /// </summary>
        public RelayCommand MoveAllFromSelectedColumnsToColumns
        {
            get
            {
                if (_moveAllFromSelectedColumnsToColumns == null)
                    _moveAllFromSelectedColumnsToColumns = new RelayCommand(MoveAllFromSelectedColumnsToColumnsAction);

                return _moveAllFromSelectedColumnsToColumns;
            }
        }

        private RelayCommand _moveAllFromColumnsToSelectedColumns;
        /// <summary>
        /// Move all values from 'Columns' list box to 'Selected Columns' list box
        /// </summary>
        public RelayCommand MoveAllFromColumnsToSelectedColumns
        {
            get
            {
                if (_moveAllFromColumnsToSelectedColumns == null)
                    _moveAllFromColumnsToSelectedColumns = new RelayCommand(MoveAllFromColumnsToSelectedColumnsAction);

                return _moveAllFromColumnsToSelectedColumns;
            }
        }

        private RelayCommand _moveFromSelectedColumnsToColumns;
        /// <summary>
        /// Move selected values form 'Selected Columns' list box to 'Columns' list box
        /// </summary>
        public RelayCommand MoveFromSelectedColumnsToColumns
        {
            get
            {
                if (_moveFromSelectedColumnsToColumns == null)
                    _moveFromSelectedColumnsToColumns = new RelayCommand(MoveFromSelectedColumnsToColumnsAction);

                return _moveFromSelectedColumnsToColumns;
            }
        }

        private RelayCommand _moveFromColumnsToSelectedColumns;
        /// <summary>
        /// Move selected values form 'Columns' list box to 'Selected Columns' list box
        /// </summary>
        public RelayCommand MoveFromColumnsToSelectedColumns
        {
            get
            {
                if (_moveFromColumnsToSelectedColumns == null)
                    _moveFromColumnsToSelectedColumns = new RelayCommand(MoveFromColumnsToSelectedColumnsAction);

                return _moveFromColumnsToSelectedColumns;
            }
        }

        private RelayCommand<SelectionChangedEventArgs> _columnsSelectionChanged;
        /// <summary>
        /// Selection changed in 'Columns' list box
        /// </summary>
        public RelayCommand<SelectionChangedEventArgs> ColumnsSelectionChanged
        {
            get
            {
                if (_columnsSelectionChanged == null)
                    _columnsSelectionChanged =
                        new RelayCommand<SelectionChangedEventArgs>(ColumnsSelectionChangedAction);

                return _columnsSelectionChanged;
            }
        }

        private RelayCommand<SelectionChangedEventArgs> _selectedColumnsSelectionChanged;
        /// <summary>
        /// Selection changed in 'Selected Columns' list box
        /// </summary>
        public RelayCommand<SelectionChangedEventArgs> SelectedColumnsSelectionChanged
        {
            get
            {
                if (_selectedColumnsSelectionChanged == null)
                    _selectedColumnsSelectionChanged =
                        new RelayCommand<SelectionChangedEventArgs>(SelectedColumnsSelectionChangedAction);

                return _selectedColumnsSelectionChanged;
            }
        }

        /// <summary>
        /// When user clicks on "Find Duplicates" button
        /// </summary>
        private RelayCommand _findDuplicatesBtnClick;
        public RelayCommand FindDuplicatesBtnClick
        {
            get 
            {
                if (_findDuplicatesBtnClick == null)
                    _findDuplicatesBtnClick = new RelayCommand(FindDuplicatesBtnClickAction);

                return _findDuplicatesBtnClick;
            }
        }

        private RelayCommand<DataGridRowEventArgs> _onLoadingRow;
        public RelayCommand<DataGridRowEventArgs> OnLoadingRow
        {
            get
            {
                if (_onLoadingRow == null)
                    _onLoadingRow = new RelayCommand<DataGridRowEventArgs>(OnLoadingRowAction);

                return _onLoadingRow;
            }
        }

        private RelayCommand _deleteBtnClick;
        public RelayCommand DeleteBtnClick
        {
            get
            {
                if (_deleteBtnClick == null)
                    _deleteBtnClick = new RelayCommand(DeleteButtonClickAction);

                return _deleteBtnClick;
            }
        }

        private RelayCommand _hideBtnClick;
        public RelayCommand HideBtnClick
        {
            get
            {
                if (_hideBtnClick == null)
                    _hideBtnClick = new RelayCommand(HideButtonClickAction);

                return _hideBtnClick;
            }
        }

        private RelayCommand _mergeBtnClick;
        public RelayCommand MergeBtnClick
        {
            get
            {
                if (_mergeBtnClick == null)
                    _mergeBtnClick = new RelayCommand(MergeButtonClickAction);

                return _mergeBtnClick;
            }
        }

        #endregion

        #region Command Actions

        /// <summary>
        /// Move all values from 'Selected Columns' list box to 'Columns' list box
        /// </summary>
        private void MoveAllFromSelectedColumnsToColumnsAction()
        {
            for (int i = 0; i < SelectedColumns.Count; )
            {
                Columns.Add(SelectedColumns.ElementAt(i));
                SelectedColumns.RemoveAt(i);
            }
        }

        /// <summary>
        /// Move all values from 'Columns' list box to 'Selected Columns' list box
        /// </summary>
        private void MoveAllFromColumnsToSelectedColumnsAction()
        {
            for (int i = 0; i < Columns.Count; )
            {
                SelectedColumns.Add(Columns.ElementAt(i));
                Columns.RemoveAt(i);
            }
        }

        /// <summary>
        /// Move selected values form 'Selected Columns' list box to 'Columns' list box
        /// </summary>
        private void MoveFromSelectedColumnsToColumnsAction()
        {
            for (int i = 0; i < _selectedInSelectedColumns.Count; )
            {
                Columns.Add(_selectedInSelectedColumns.ElementAt(i));
                SelectedColumns.Remove(_selectedInSelectedColumns.ElementAt(i));
            }
        }

        /// <summary>
        /// Move selected values form 'Columns' list box to 'Selected Columns' list box
        /// </summary>
        private void MoveFromColumnsToSelectedColumnsAction()
        {
            for (int i = 0; i < _selectedInColumns.Count; )
            {
                SelectedColumns.Add(_selectedInColumns.ElementAt(i));
                Columns.Remove(_selectedInColumns.ElementAt(i));
            }
        }

        /// <summary>
        /// Selection changed in 'Columns' list box
        /// </summary>
        private void ColumnsSelectionChangedAction(SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                _selectedInColumns.Add(e.AddedItems[0] as string);
            else
                _selectedInColumns.Remove(e.RemovedItems[0] as string);
        }

        /// <summary>
        /// Selection changed in 'Selected Columns' list box
        /// </summary>
        private void SelectedColumnsSelectionChangedAction(SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                _selectedInSelectedColumns.Add(e.AddedItems[0] as string);
            else
                _selectedInSelectedColumns.Remove(e.RemovedItems[0] as string);
        }

        /// <summary>
        /// Action for "Find duplicates" button click
        /// </summary>
        private void FindDuplicatesBtnClickAction()
        {
            ProgressBarVisibility = Visibility.Visible;

            // Get DataTable from cache of contacts
            DataTable table = ContactExtension.GetDataTableFromContacts();

            Deduplication dedup = new Deduplication(new ProgressNotifyDelegate(OnProgress));
            // Build columns list for deduplication
            List<Column> columns = new List<Column>(table.Columns.Count);

            Column column;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                column = new Column(table.Columns[i].ColumnName);
                column.SearchIn = SelectedColumns.Contains(column.Name);
                columns.Add(column);
            }

            // Search duplicates
            DedupTable = dedup.SearchDuplicates(table, columns, null, SeacrhQuality.Normal,
                _thresholdLevel, ReturnResults.DuplicatesOnly);

            TotalDuplicates = "Total Duplicates: " + _dedupTable.Rows.Count;
        }

        private void OnLoadingRowAction(DataGridRowEventArgs e)
        {
            // Get the DataRow corresponding to the DataGridRow that is loading.
            DataRowView item = e.Row.Item as DataRowView;
            if (item != null)
            {
                DataRow row = item.Row;

                if (_lastId == null)
                    _lastId = row["_ID"] as string;

                if (row["_ID"] as string != _lastId)
                {
                    if (_lastColor == Colors.LightBlue)
                        _lastColor = Colors.White;
                    else
                        _lastColor = Colors.LightBlue;
                }

                e.Row.Background = new SolidColorBrush(_lastColor);
                _lastId = row["_ID"] as string;
            }
        }

        /// <summary>
        /// On "Delete" button click
        /// </summary>
        private void DeleteButtonClickAction()
        {
            Guid contactId = Guid.Parse(_dedupTable.Rows[DataSelectedIndex]["_ContactID"].ToString());
            ContactsManager.Current.Delete(ContactsManager.Current.ContactsCache.Single(c => c.ContactID == contactId));
            DedupTable.Rows.RemoveAt(DataSelectedIndex);            
        }

        private void HideButtonClickAction()
        {
            DedupTable.Rows.RemoveAt(DataSelectedIndex);
        }

        private void MergeButtonClickAction()
        {
            Guid   contactId = Guid.Parse(_dedupTable.Rows[DataSelectedIndex]["_ContactID"].ToString());
            string groupdID  = _dedupTable.Rows[DataSelectedIndex]["_ID"].ToString();
            List<Contact> contacts = new List<Contact>();
            
            // Finding rows from group
            DataRow[] findedRows = _dedupTable.Select("_ID = '" + groupdID + "'");

            foreach (DataRow row in findedRows)
            {
                // If clicked row
                if (row["_ContactID"].ToString().Equals(contactId.ToString()))
                    continue;

                // Add contact to list of deleted contacts
                contacts.Add(ContactsManager.Current.ContactsCache.Single(c => c.ContactID.ToString() == row["_ContactID"].ToString()));

                // Remove contacts
                _dedupTable.Rows.Remove(row);
            }

            // Remove contacts from database
            ContactsManager.Current.Delete(contacts);
        }

        private void OnProgress(int progress)
        {
            PercentsDone = progress;
        }

        #endregion
    }
}
