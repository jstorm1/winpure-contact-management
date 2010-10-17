#region References

using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Common.Helpers;

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers.DataManagers.Base
{
    /// <summary>
    /// Base class for the DataManagers.
    /// </summary>
    public class DataManagerBase:PropertyChangedBase 
    {
        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected DataManagerBase()
        {
            if (Context == null)
                Context = new EntitiesDataContext();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns current context instance.
        /// </summary>
        protected static EntitiesDataContext Context { get; set; }

        #endregion
    }
}