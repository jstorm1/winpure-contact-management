using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using WinPure.ContactManagement.Client.Data.Model.Extensions;
using WinPure.ContactManagement.Client.Data.Model;

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

#endregion

#region Constructor

        public DeduplicationViewModel()
        {
            Columns         = new ObservableCollection<string>();
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

#endregion

#region Commands

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

#endregion

#region Command Actions

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

#endregion
    }
}
