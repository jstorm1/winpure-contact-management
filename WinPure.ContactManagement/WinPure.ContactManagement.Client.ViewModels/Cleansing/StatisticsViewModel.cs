using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Data.Model.Extensions;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Common.Helpers;
using Visifire.Charts;
using System.Windows.Input;
using System.Windows;


namespace WinPure.ContactManagement.Client.ViewModels.Cleansing
{
    public enum ColumnScoreOption { Populated, Empty };

    public class ColumnScore
    {
        public float Score { get; set; }
        public string Name { get; set; }
        public string PercentedScore { get; set; }


        public ColumnScore(string name, float score)
        {
            Name = name;
            Score = score;
            PercentedScore = String.Format("{0:0.##}%", score);
        }
    }

    public class StatisticsViewModel : Base.ViewModelBase
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

        #endregion

        #region Constructor

        public StatisticsViewModel()
        {
            Columns = new ObservableCollection<string>();
            SelectedColumns = new ObservableCollection<string>(ContactExtension.GetContactsColumnNames(new Contact()));
            ColumnsScore = new ObservableCollection<ColumnScore>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Type of selected chart
        /// </summary>
        public int ChartType { get; set; }

        /// <summary>
        /// Populated or empty fields
        /// </summary>        
        public int CellsOption { get; set; }

        /// <summary>
        /// Data for 'Columns' list box
        /// </summary>
        public ObservableCollection<string> Columns { get; set; }

        /// <summary>
        /// Data for 'Selected Columns' list box
        /// </summary>
        public ObservableCollection<string> SelectedColumns { get; set; }

        /// <summary>
        /// Data for DataGrid which contains scores of columns
        /// </summary>
        public ObservableCollection<ColumnScore> ColumnsScore { get; set; }

        private Visibility _dataTabItemVisibility = Visibility.Collapsed;
        public Visibility DataTabItemVisibility
        {
            get { return _dataTabItemVisibility; }
            set
            {
                _dataTabItemVisibility = value;
                RaisePropertyChanged("DataTabItemVisibility");
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

        private RelayCommand _refreshButtonClick;
        public RelayCommand RefreshButtonClick
        {
            get
            {
                if (_refreshButtonClick == null)
                    _refreshButtonClick = new RelayCommand(OnRefreshButtonClick);

                return _refreshButtonClick;
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

        private void OnRefreshButtonClick()
        {
            ColumnsScore.Clear();

            foreach (string column in SelectedColumns)
            {
                ColumnsScore.Add(new ColumnScore(column,
                    GetScoreOfAColumn(column, (ColumnScoreOption)CellsOption)));
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns a percentile value of column filling
        /// </summary>
        /// <param name="column">Column name</param>
        /// <param name="option">Populated or empty filled</param>
        /// <returns>Percentile value of filling (94% for example)</returns>
        private float GetScoreOfAColumn(string column, ColumnScoreOption option)
        {
            // Number of relevant fields
            float fields = 0;
            SynchronizedObservableCollection<Contact> contacts = ContactsManager.Current.ContactsCache;

            // If contacts is not empty
            if (contacts.Count > 0)
            {
                // In every contact
                foreach (Contact contact in contacts)
                {
                    // Get value of specified column
                    string fieldValue = contact.GetType().GetProperty(column).GetValue(contact, null) as string;

                    // For non empty cells
                    if (option == ColumnScoreOption.Populated)
                    {
                        if (!string.IsNullOrEmpty(fieldValue))
                            fields++;
                    }

                    // For empty cells
                    if (option == ColumnScoreOption.Empty)
                    {
                        if (string.IsNullOrEmpty(fieldValue))
                            fields++;
                    }
                }

                float onePercent = ((float)contacts.Count) / 100;

                // Count of fields / 1%
                return fields / onePercent;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}