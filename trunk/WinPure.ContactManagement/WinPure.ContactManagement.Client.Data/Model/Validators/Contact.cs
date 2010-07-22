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

        partial void OnLastNameChanged()
        {
            if (string.IsNullOrEmpty(_LastName))
            {
                AddError("LastName", "Last Name can't be empty.");
            }
            else
            {
                RemoveError("LastName");
            }
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
