#region References

using System;
using System.Globalization;
using System.Threading;
using WinPure.ContactManagement.Common.Helpers; 

#endregion

namespace WinPure.ContactManagement.Client.Localization
{
    public sealed class LanguageContext : PropertyChangedBase
    {
        #region Fields

        private static LanguageContext _instance;

        private CultureInfo _cultureInfo;
        private LanguageDictionary _dictionary;

        #endregion

        #region Sigleton Constructor
        
        private LanguageContext()
        {
        }

        public static LanguageContext Current
        {
            get { return _instance ?? (_instance = new LanguageContext()); }
        } 

        #endregion

        #region Properties

        public CultureInfo Culture
        {
            get { return _cultureInfo ?? (_cultureInfo = CultureInfo.CurrentUICulture); }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Culture must not be null.");
                }
                if (value == _cultureInfo)
                {
                    return;
                }
                if (_cultureInfo != null)
                {
                    LanguageDictionary currentDictionary = LanguageDictionary.GetDictionary(_cultureInfo);
                    currentDictionary.Unload();
                }
                _cultureInfo = value;
                LanguageDictionary newDictionary = LanguageDictionary.GetDictionary(_cultureInfo);
                Thread.CurrentThread.CurrentUICulture = _cultureInfo;
                newDictionary.Load();
                Dictionary = newDictionary;
                RaisePropertyChanged("Culture");
            }
        }

        public LanguageDictionary Dictionary
        {
            get { return _dictionary; }
            set
            {
                if (value != null && value != _dictionary)
                {
                    _dictionary = value;
                    RaisePropertyChanged("Dictionary");
                }
            }
        }

        #endregion
    }
}