using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Model.Extensions;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.DeduplicationModule;
using System.Data;

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
            set { _thresholdLevel = value; }
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
            DataTable table = ContactExtension.GetDataTableFromContacts();
        }

        #endregion
    }
}
