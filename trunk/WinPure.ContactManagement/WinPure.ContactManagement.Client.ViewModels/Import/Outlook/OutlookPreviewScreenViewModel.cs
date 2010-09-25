using Microsoft.Office.Interop.Outlook;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.Outlook
{
    public class OutlookPreviewScreenViewModel : ViewModelBase
    {
        private Folders _outlookFolders;

        public OutlookPreviewScreenViewModel()
        {
            OutlookFolders = OutlookImportManager.Current.GetContactsFolders();
        }

        public Folders OutlookFolders
        {
            get { return _outlookFolders; }
            set
            {
                if (_outlookFolders == value) return;
                _outlookFolders = value;
                RaisePropertyChanged("OutlookFolders");
            }
        }
    }
}