#region References

using WinPure.ContactManagement.Client.Data.Model.Validators.Base;

#endregion

namespace WinPure.ContactManagement.Client.Data.Model
{
    public partial class SyncServerConnection : EntityValidatorBase
    {
        partial void OnNameChanged()
        {
            if (string.IsNullOrEmpty(_Name))
            {
                AddError("Name", "Please enter connection name.");
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