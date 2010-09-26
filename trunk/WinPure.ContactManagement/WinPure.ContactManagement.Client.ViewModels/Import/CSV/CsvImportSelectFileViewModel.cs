using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.CSV
{
    public class CsvImportSelectFileViewModel : ViewModelBase
    {
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName == value) return;
                _fileName = value;
                RaisePropertyChanged("FileName");
            }
        }
    }
}