using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using WinPure.ContactManagement.Client.Data.Managers.Import;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Import.Excel
{
    public class SelectFileViewModel : ViewModelBase
    {
        private string _fileName;
        private RelayCommand _openFileCommand;
        private bool _readyToContinue;

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

        public bool ReadyToContinue
        {
            get { return _readyToContinue; }
            set
            {
                if (_readyToContinue == value) return;
                _readyToContinue = value;
                RaisePropertyChanged("ReadyToContinue");
            }
        }

        private void openFile()
        {
            var dialog = new OpenFileDialog { CheckFileExists = true, Filter = "Excel Files (*.xls; *.xlsx)|*.xls;*.xlsx" };
            if (dialog.ShowDialog() != true) return;

            FileName = dialog.FileName;
            ExcelImportManager.Current.SetCurrentFile(FileName);
            ReadyToContinue = true;
        }
    }
}