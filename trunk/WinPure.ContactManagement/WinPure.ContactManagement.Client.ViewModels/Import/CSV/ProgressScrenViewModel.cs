﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.CSV
{
    public class ProgressScrenViewModel:ViewModelBase 
    {
        private bool _isSelected;
        private RelayCommand _startImportCommand;
        private int _completedPercent;
        private bool _isImportCompleted;

        public RelayCommand StartImportCommand
        {
            get { return _startImportCommand ?? (_startImportCommand = new RelayCommand(startImport)); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                RaisePropertyChanged("IsSelected");

                if (IsSelected) initialize();
            }
        }

        public int CompletedPercent
        {
            get { return _completedPercent; }
            set
            {
                if(_completedPercent == value) return;
                _completedPercent = value;
                RaisePropertyChanged("CompletedPercent");
            }
        }

        public bool IsImportCompleted
        {
            get { return _isImportCompleted; }
            set
            {
                if(_isImportCompleted == value) return;
                _isImportCompleted = value;
                RaisePropertyChanged("IsImportCompleted");
            }
        }

        private void initialize()
        {
            CsvImportManager.Current.ImportProgressChanged += onCsvImportManagerImportProgressChanged;
            CsvImportManager.Current.ImportProgressCompleted += onCsvImportManagerImportProgressCompleted;

            startImport();
        }

        void onCsvImportManagerImportProgressCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsImportCompleted = true;
        }

        void onCsvImportManagerImportProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CompletedPercent = e.ProgressPercentage;
        }

        private static void startImport()
        {
            CsvImportManager.Current.StartImport();
        }
    }
}
