using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinPure.ContactManagement.Client.Data.Model.Validators.Base;

namespace WinPure.ContactManagement.Client.Data.Model
{
    public partial class Company:EntityValidatorBase 
    {
        partial void OnNameChanged()
        {
            if (string.IsNullOrEmpty(_Name))
            {
                AddError("Name", "Company name can't be empty.");
            }
            else
            {
                RemoveError("Name");
            }
        }

        partial void OnCountryChanged()
        {
            if (string.IsNullOrEmpty(_Country))
            {
                AddError("Country", "Country can't be empty.");
            }
            else
            {
                RemoveError("Country");
            }
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
