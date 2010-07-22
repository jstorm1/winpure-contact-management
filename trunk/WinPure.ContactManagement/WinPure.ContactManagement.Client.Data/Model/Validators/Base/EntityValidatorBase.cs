#region References

using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Objects.DataClasses;

#endregion

namespace WinPure.ContactManagement.Client.Data.Model.Validators.Base
{
    public abstract class EntityValidatorBase :EntityObject, IDataErrorInfo
    {
        #region Errors Collection

        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

        public bool HasErrors
        {
            get { return _validationErrors.Count > 0; }
        }

        protected void AddError(string columnName, string message)
        {
            if (!_validationErrors.ContainsKey(columnName))
            {
                _validationErrors.Add(columnName, message);
            }
        }

        protected void RemoveError(string columnName)
        {
            if (_validationErrors.ContainsKey(columnName))
            {
                _validationErrors.Remove(columnName);
            }
        }

        #endregion

        public abstract void Validate();

        #region Implementation of IDataErrorInfo

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <returns>
        /// The error message for the property. The default is an empty string ("").
        /// </returns>
        /// <param name="columnName">The name of the property whose error message to get. </param>
        public string this[string columnName]
        {
            get {
                return !_validationErrors.ContainsKey(columnName)
                           ? null
                           : (_validationErrors.Count > 0 ? _validationErrors[columnName] : null);
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>
        /// An error message indicating what is wrong with this object. The default is an empty string ("").
        /// </returns>
        public string Error
        {
            get { return _validationErrors.Count > 0 ? "Object is invalid." : null; }
        }

        #endregion
    }
}
