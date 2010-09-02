#region References

using System;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Transitionals;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.ViewModels.Base;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels.Settings
{
    public class ViewTabViewModel : ViewModelBase
    {
        #region Fields

        private RelayCommand _cancelCommand;
        private bool _contentSwitcher;
        private RelayCommand _saveCommand;
        private Transition _selectedTransition;
        private Type _selectedType;
        private string _selectedTypeName;
        private RelayCommand _swapCellsCommand;

        #endregion

        #region Constructor

        public ViewTabViewModel()
        {
            if (TransitionsManager.Current.CurrentTransition != null)
            {
                SelectedType = TransitionsManager.Current.CurrentTransition.GetType();
            }

            TransitionsManager.Current.CurrentTransitionChanged +=
                delegate
                    {
                        if (TransitionsManager.Current.CurrentTransition != null)
                            SelectedType = TransitionsManager.Current.CurrentTransition.GetType();
                    };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of known transition types.
        /// </summary>
        /// <value>
        /// The list of known transition types.
        /// </value>
        public ObservableCollection<Type> TransitionTypes
        {
            get { return TransitionsManager.Current.TransitionTypes; }
        }

        /// <summary>
        /// Gets or Sets Currently selected transition type.
        /// </summary>
        public Type SelectedType
        {
            get { return _selectedType; }
            set
            {
                if (_selectedType == value) return;
                _selectedType = value;

                RaisePropertyChanged("SelectedType");

                SelectedTransition = TransitionsManager.Current.GetTransition(_selectedType);
                SelectedTypeName = _selectedType.Name;
                swapCells();
            }
        }

        /// <summary>
        /// Gets Currently selected transition type.
        /// </summary>
        public Transition SelectedTransition
        {
            get { return _selectedTransition; }
            private set
            {
                _selectedTransition = value;
                RaisePropertyChanged("SelectedTransition");
            }
        }

        public string SelectedTypeName
        {
            get { return _selectedTypeName; }
            set
            {
                if (_selectedTypeName == value) return;
                _selectedTypeName = value;
                RaisePropertyChanged("SelectedTypeName");
            }
        }

        /// <summary>
        /// If false then First page must be visible,
        /// if true then second page must be visible.
        /// </summary>
        public bool ContentSwitcher
        {
            get { return _contentSwitcher; }
            set
            {
                if (_contentSwitcher == value) return;
                _contentSwitcher = value;
                RaisePropertyChanged("ContentSwitcher");
            }
        }

        public RelayCommand SwapCellsCommand
        {
            get { return _swapCellsCommand ?? (_swapCellsCommand = new RelayCommand(swapCells)); }
        }

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save)); }
        }

        public RelayCommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new RelayCommand(Cancel)); }
        }

        #endregion

        #region Methods

        private void Save()
        {
            TransitionsManager.Current.ActivateTransition(SelectedType);
            TransitionsManager.Current.SaveSettings();
        }

        private void Cancel()
        {
            MessageBox.Show("Not Implemented");
        }

        private void swapCells()
        {
            ContentSwitcher = !ContentSwitcher;
        }

        #endregion
    }
}