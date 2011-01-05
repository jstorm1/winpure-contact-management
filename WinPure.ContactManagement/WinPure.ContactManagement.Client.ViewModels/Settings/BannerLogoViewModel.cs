using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Settings
{
    public class BannerLogoViewModel : ViewModelBase
    {
        private string _fileName;
        private RelayCommand _openFileCommand;
        private ImageSource _previewSource;

        public BannerLogoViewModel()
        {
            FileName = BannerLogoManager.Current.CurrentLogoPath;
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName == value) return;

                _fileName = value;
                RaisePropertyChanged("FileName");
                
                BannerLogoManager.Current.CurrentLogoPath = value;
                BannerLogoManager.Current.Save();
                PreviewSource = BannerLogoManager.Current.GetImageSource();
            }
        }

        public RelayCommand OpenFileCommand
        {
            get { return _openFileCommand ?? (_openFileCommand = new RelayCommand(openFile)); }
        }

        public ImageSource PreviewSource
        {
            get { return _previewSource; }
            set
            {
                if (_previewSource == value)return;
                _previewSource = value;
                RaisePropertyChanged("PreviewSource");
            }
        }

        private void openFile()
        {
            var dialog = new OpenFileDialog {CheckFileExists = true, Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png"};
            if (dialog.ShowDialog() != true) return;

            FileName = dialog.FileName;
        }
    }
}