#region References

using WinPure.ContactManagement.Client.Data.Model.Validators.Base;
using WinPure.ContactManagement.Client.Localization;

#endregion

namespace WinPure.ContactManagement.Client.Data.Model
{
    public partial class Company : EntityValidatorBase
    {
        partial void OnNameChanged()
        {
            if (string.IsNullOrEmpty(_Name))
                AddError("Name", LanguageDictionary.CurrentDictionary.Translate<string>("Validation.CompanyName", "Message"));
            else
                RemoveError("Name");
        }

        partial void OnCountryChanged()
        {
            if (string.IsNullOrEmpty(_Country))
                AddError("Country", LanguageDictionary.CurrentDictionary.Translate<string>("Validation.Country", "Message"));
            else
                RemoveError("Country");
        }

        #region Overrides of ValidatorBase

        public override void Validate()
        {
            OnNameChanged();
            OnCountryChanged();
        }

        #endregion
    }
}