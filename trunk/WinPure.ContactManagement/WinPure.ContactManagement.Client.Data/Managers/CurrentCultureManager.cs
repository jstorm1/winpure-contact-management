using System.Linq;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Common;

namespace WinPure.ContactManagement.Client.Data.Managers
{
    public class CurrentCultureManager
    {
        private Setting _cultureSetting;
        private string _currentCultureName;

        #region Sigleton Constructor

        private static CurrentCultureManager _instance;

        private CurrentCultureManager()
        {
            _cultureSetting = loadCultureSettings();
            CurrentCultureName = _cultureSetting.Value;
        }

        public static CurrentCultureManager Current
        {
            get { return _instance ?? (_instance = new CurrentCultureManager()); }
        }

        #endregion

        public string CurrentCultureName
        {
            get { return _currentCultureName; }
            set { _currentCultureName = value; }
        }

        Setting loadCultureSettings()
        {
            var settingsCache = SettingsManager.Current.LoadSettings();

            var cultureName = settingsCache.Where(s => s.Name == SettingsConstants.CURRENT_CULTURE_NAME).FirstOrDefault();
            if (cultureName == null)
            {
                SettingsManager.Current.AddSetting(SettingsConstants.CURRENT_CULTURE_NAME, "");
                cultureName = loadCultureSettings();
            }

            return cultureName;
        }

        public void Save()
        {
            if (_cultureSetting == null)
            {
                SettingsManager.Current.AddSetting(SettingsConstants.CURRENT_CULTURE_NAME,CurrentCultureName);
                return;
            }

            _cultureSetting.Value = CurrentCultureName;
            SettingsManager.Current.Save(_cultureSetting);
        }
    }
}