#region References

using System;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Linq;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers.Base;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Common;

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers.DataManagers
{
    public class SettingsManager : DataManagerBase
    {
        private ObservableCollection<Setting> _settingsCache;

        #region Singleton Constructor

        private static SettingsManager _instance;

        private SettingsManager()
        {
        }

        /// <summary>
        /// Returns current instance.
        /// </summary>
        public static SettingsManager Current
        {
            get { return _instance ?? (_instance = new SettingsManager()); }
        }

        #endregion

        public ObservableCollection<Setting> LoadSettings()
        {
            if (_settingsCache == null)
                RefreshCache();

            return _settingsCache;
        }

        public void Save()
        {
            if (_settingsCache == null) return;

            Context.SaveChanges();
            RefreshCache();
        }

        public void AddSetting(string name, string value)
        {
            var setting = new Setting
                              {
                                  SettingId = Guid.NewGuid(),
                                  Name = name,
                                  Value = value
                              };
            Context.AddToSettings(setting);
            Context.SaveChanges();

            RefreshCache();
        }

        public void Revert()
        {
            RefreshCache();
        }

        public void RefreshCache()
        {
            Context.Refresh(RefreshMode.StoreWins, Context.Settings);

            if (_settingsCache == null)
                _settingsCache = new ObservableCollection<Setting>();

            _settingsCache.Clear();

            foreach (Setting setting in Context.Settings)
            {
                _settingsCache.Add(setting);
            }
        }

    }
}