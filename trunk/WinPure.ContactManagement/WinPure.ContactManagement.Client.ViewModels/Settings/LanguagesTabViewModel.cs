using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Localization;
using WinPure.ContactManagement.Client.ViewModels.Base;

namespace WinPure.ContactManagement.Client.ViewModels.Settings
{
    public class LanguagesTabViewModel:ViewModelBase
    {
        private RelayCommand _setEnglishLanguageCommand;
        private RelayCommand _setFrenchLanguageCommand;
        private RelayCommand _setSpanishLanguageCommand;
        private RelayCommand _setItalianLanguageCommand;


        public RelayCommand SetEnglishLanguageCommand
        {
            get { return _setEnglishLanguageCommand ?? (_setEnglishLanguageCommand = new RelayCommand(setEnglishLanguage)); }
        }

        public RelayCommand SetFrenchLanguageCommand
        {
            get { return _setFrenchLanguageCommand ?? (_setFrenchLanguageCommand = new RelayCommand(setFrenchLanguage)); }
        }

        public RelayCommand SetSpanishLanguageCommand
        {
            get { return _setSpanishLanguageCommand ?? (_setSpanishLanguageCommand = new RelayCommand(setSpanishLanguage)); }
        }

        public RelayCommand SetItalianLanguageCommand
        {
            get { return _setItalianLanguageCommand ?? (_setItalianLanguageCommand = new RelayCommand(setItalianLanguage)); }
        }

        private void setItalianLanguage()
        {
            saveCurrentCulture("it-IT");	
        }

        private void setSpanishLanguage()
        {
            saveCurrentCulture("es-ES");	
        }

        private void setFrenchLanguage()
        {
            saveCurrentCulture("fr-FR");	
        }

        private void setEnglishLanguage()
        {
            saveCurrentCulture("en-US");	
        }

        private void saveCurrentCulture(string cultureName)
        {
            LanguageContext.Current.Culture = CultureInfo.GetCultureInfo(cultureName);

            CurrentCultureManager.Current.CurrentCultureName = cultureName;
            CurrentCultureManager.Current.Save();
        }
    }
}
