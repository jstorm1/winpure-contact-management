using System;
using System.Collections.ObjectModel;
using Microsoft.Office.Interop.Outlook;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.Outlook
{
    public class OutlookPreviewScreenViewModel : ViewModelBase
    {
        private bool _isSelected;
        private ObservableCollection<object> _outlookFolders;
        private ObservableCollection<object> _outlookContacts;

        public ObservableCollection<object> OutlookFolders
        {
            get { return _outlookFolders; }
            set
            {
                if (_outlookFolders == value) return;
                _outlookFolders = value;
                RaisePropertyChanged("OutlookFolders");
            }
        }

        public ObservableCollection<object> OutlookContacts
        {
            get { return _outlookContacts; }
            set
            {
                if (_outlookContacts == value) return;
                _outlookContacts = value;
                RaisePropertyChanged("OutlookContacts");
            }
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

        private void initialize()
        {
            //Check for Design mode.
            if (IsDesignMode) return;

            OutlookFolders = OutlookImportManager.Current.GetContactsFolders();
            OutlookContacts = OutlookImportManager.Current.GetContactsFromFolder();
        }
    }
}