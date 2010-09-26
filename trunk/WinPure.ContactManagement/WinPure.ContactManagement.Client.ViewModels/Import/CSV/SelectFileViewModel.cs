using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.CSV
{
    public class SelectFileViewModel : ViewModelBase
    {
        private string _fileName;
        private RelayCommand _openFileCommand;

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

        public RelayCommand OpenFileCommand
        {
            get { return _openFileCommand ?? (_openFileCommand = new RelayCommand(openFile)); }
        }

        private void openFile()
        {
            var dialog = new OpenFileDialog {CheckFileExists = true};
            if (dialog.ShowDialog() != true) return;

            FileName = dialog.FileName;
            CsvImportManager.Current.SetCurrentFile(FileName);
        }
    }
}