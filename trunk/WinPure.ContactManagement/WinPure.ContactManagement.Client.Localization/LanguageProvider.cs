#region References

using System;
using System.ComponentModel;
using System.Globalization; 

#endregion

namespace WinPure.ContactManagement.Client.Localization
{
    public sealed class LanguageProvider : ISupportInitialize
    {
        #region Fields

        private CultureInfo _culture;
        private Type _dictionaryType;
        private object _parameter;

        #endregion

        #region Properties

        public Type DictionaryType
        {
            get { return _dictionaryType; }
            set { _dictionaryType = value; }
        }

        public CultureInfo Culture
        {
            get { return _culture; }
            set { _culture = value; }
        }

        public object Parameter
        {
            get { return _parameter; }
            set { _parameter = value; }
        }

        #endregion

        #region ISupportInitialize Members

        public void BeginInit()
        {
        }

        public void EndInit()
        {
            object instance = Activator.CreateInstance(_dictionaryType, new[] {_parameter});
            var dictionary = instance as LanguageDictionary;
            LanguageDictionary.RegisterDictionary(_culture, dictionary);
        }

        #endregion
    }
}