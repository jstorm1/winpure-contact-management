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

        private Chart _chartControl;
        public Chart ChartControl
        {
            get
            {
                return _chartControl;
            }
            set
            {
                _chartControl = value;
                RaisePropertyChanged("ChartControl");
            }
        }

        /// <summary>
        /// Data for 'Columns' list box
        /// </summary>
        public ObservableCollection<string> Columns { get; set; }

        /// <summary>
        /// Data for 'Selected Columns' list box
        /// </summary>
        public ObservableCollection<string> SelectedColumns { get; set; }

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

        private int _dataTabControlSelectedTab;
        public int DataTabControlSelectedTab
        {
            get
            {
                return _dataTabControlSelectedTab;
            }
            set
            {
                _dataTabControlSelectedTab = value;
                RaisePropertyChanged("DataTabControlSelectedTab");
            }
        }

        public ObservableCollection<Contact> FilteredContacts { get; set; }

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


        #endregion

        #region Helpers

        #endregion
    }
}
