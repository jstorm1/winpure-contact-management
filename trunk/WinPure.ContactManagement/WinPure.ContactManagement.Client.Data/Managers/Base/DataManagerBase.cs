#region References

using WinPure.ContactManagement.Client.Data.Model;

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers
{
    /// <summary>
    /// Base class for the DataManagers.
    /// </summary>
    public class DataManagerBase
    {
        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataManagerBase()
        {
            Context = new EntitiesDataContext();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns current context instance.
        /// </summary>
        public EntitiesDataContext Context { get; private set; }

        #endregion
    }
}