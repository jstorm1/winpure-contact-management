#region References

using WinPure.ContactManagement.Client.Data.Model.Validators.Base;
using WinPure.ContactManagement.Client.Localization;

#endregion

namespace WinPure.ContactManagement.Client.Data.Model
{
    public partial class SyncServerConnection : EntityValidatorBase
    {
        partial void OnNameChanged()
        {
            if (string.IsNullOrEmpty(_Name))
            {
                AddError("Name", LanguageDictionary.CurrentDictionary.Translate<string>("Validation.ConnectionName", "Message"));
            }
            else
            {
                RemoveError("Name");
            }
        }

        #region Overrides of ValidatorBase

        public override void Validate()
        {
            OnNameChanged();
        }

        #endregion
    }
}