#region References

using WinPure.ContactManagement.Client.Data.Model.Validators.Base;
using WinPure.ContactManagement.Client.Localization;

#endregion

namespace WinPure.ContactManagement.Client.Data.Model
{
    public partial class Contact : EntityValidatorBase
    {
        partial void OnFirstNameChanged()
        {
            if (string.IsNullOrEmpty(_FirstName))
                AddError("FirstName", LanguageDictionary.CurrentDictionary.Translate<string>("Validation.FirstName", "Message"));
            else
                RemoveError("FirstName");
        }

        partial void OnLastNameChanged()
        {
            if (string.IsNullOrEmpty(_LastName))
                AddError("LastName", LanguageDictionary.CurrentDictionary.Translate<string>("Validation.LastName", "Message"));
            else
                RemoveError("LastName");
        }

        #region Overrides of EntityValidatorBase

        public override void Validate()
        {
            OnFirstNameChanged();
            OnLastNameChanged();
        }

        #endregion
    }
}