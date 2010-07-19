#region References

using System.Collections.Generic;
using System.ComponentModel; 

#endregion

namespace WinPure.ContactManagement.Client.Data.Model
{
    public partial class SyncServerConnection : IDataErrorInfo
    {
        #region Errors Collection

        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

        public bool HasErrors
        {
            get { return _validationErrors.Count > 0; }
        }

        private void addError(string columnName, string message)
        {
            if (!_validationErrors.ContainsKey(columnName))
            {
                _validationErrors.Add(columnName, message);
            }
        }

        private void removeError(string columnName)
        {
            if (_validationErrors.ContainsKey(columnName))
            {
                _validationErrors.Remove(columnName);
            }
        }

        #endregion

        partial void OnNameChanged()
        {
            if (string.IsNullOrEmpty(_Name))
            {
                addError("Name", "Please enter connection name.");
            }
            else
            {
                removeError("Name");
            }
        }

        public void Validate()
        {
            OnNameChanged();
        }

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
            get { return _validationErrors.Count > 0 ? _validationErrors[columnName] : null; }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>
        /// An error message indicating what is wrong with this object. The default is an empty string ("").
        /// </returns>
        public string Error
        {
            get { return _validationErrors.Count > 0 ? "Sync Server Connection is invalid." : null; }
        }

        #endregion
    }
}