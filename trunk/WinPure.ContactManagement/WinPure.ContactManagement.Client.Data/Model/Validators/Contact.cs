#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinPure.ContactManagement.Client.Data.Model.Validators.Base;

#endregion

namespace WinPure.ContactManagement.Client.Data.Model
{
    public partial class Contact : EntityValidatorBase
    {
        partial void OnFirstNameChanged()
        {
            if (string.IsNullOrEmpty(_FirstName))
            {
                AddError("FirstName", "First Name can't be empty.");
            }
            else
            {
                RemoveError("FirstName");
            }
        }

        #region Overrides of EntityValidatorBase

        public override void Validate()
        {
            OnFirstNameChanged();
        }

        #endregion


    }
}
