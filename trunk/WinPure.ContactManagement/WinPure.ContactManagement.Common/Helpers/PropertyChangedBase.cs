#region References

using System.ComponentModel;

#endregion

namespace WinPure.ContactManagement.Common.Helpers
{
    /// <summary>
    /// Base class for classes which needs to call PropertyChanged event.
    /// </summary>
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Method which invokes <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this,
                        new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}