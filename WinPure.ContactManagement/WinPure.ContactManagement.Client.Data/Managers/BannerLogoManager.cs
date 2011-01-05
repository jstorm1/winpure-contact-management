using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Common;

namespace WinPure.ContactManagement.Client.Data.Managers
{
    public class BannerLogoManager
    {
        public event EventHandler LogoChanged;

        private readonly Setting _logoSetting;
        private string _currentLogoPath;

        #region Singleton Constructor

        private static BannerLogoManager _instance;

        private BannerLogoManager()
        {
            _logoSetting = loadLogoSettings();
            CurrentLogoPath = _logoSetting.Value;
        }

        public static BannerLogoManager Current
        {
            get { return _instance ?? (_instance = new BannerLogoManager()); }
        }

        #endregion

        public ImageSource GetImageSource()
        {
            ImageSource imageSource;
            FileInfo file;

            try
            {
                file = new FileInfo(Current.CurrentLogoPath);
            }
            catch (Exception)
            {
                file = null;
            }
            
            if (file == null || !file.Exists || (file.Extension.ToUpper() != ".PNG" && file.Extension.ToUpper() != ".JPG"))
            {
                imageSource = new BitmapImage(new Uri("/WinPure.ContactManagement.Client;component/Resources/Images/Logo_Header_450x80.png", UriKind.Relative));
                return imageSource;
            }

            imageSource = new BitmapImage(new Uri(file.FullName, UriKind.Absolute));
            return imageSource;
        }

        public string CurrentLogoPath
        {
            get { return _currentLogoPath; }
            set
            {
                if (_currentLogoPath == value) return;
                _currentLogoPath = value;

                if (LogoChanged != null)
                    LogoChanged.Invoke(this, EventArgs.Empty);
            }
        }

        private Setting loadLogoSettings()
        {
            ObservableCollection<Setting> settingsCache = SettingsManager.Current.LoadSettings();

            Setting logoPath = settingsCache.Where(s => s.Name == SettingsConstants.CURRENT_LOGO_PATH).FirstOrDefault();
            if (logoPath == null)
            {
                SettingsManager.Current.AddSetting(SettingsConstants.CURRENT_LOGO_PATH, "");
                logoPath = loadLogoSettings();
            }

            return logoPath;
        }

        public void Save()
        {
            if (_logoSetting == null)
            {
                SettingsManager.Current.AddSetting(SettingsConstants.CURRENT_LOGO_PATH, CurrentLogoPath);
                return;
            }

            _logoSetting.Value = CurrentLogoPath;
            SettingsManager.Current.Save(_logoSetting);
        }
    }
}