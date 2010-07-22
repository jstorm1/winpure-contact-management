#region References

using System;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Linq;
using WinPure.ContactManagement.Client.Data.Model; 

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers
{
    public class SyncServerConnectionsManager:DataManagerBase
    {
        private ObservableCollection<SyncServerConnection> _syncServerConnectionsCache;
        
        #region Singleton Constructor
        
        private static SyncServerConnectionsManager _instance;

        private SyncServerConnectionsManager()
        {

        }

        /// <summary>
        /// Returns Current instance.
        /// </summary>
        public static SyncServerConnectionsManager Current
        {
            get { return _instance ?? (_instance = new SyncServerConnectionsManager()); }
        } 

        #endregion

        /// <summary>
        /// Methods for loading connections list from database.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<SyncServerConnection> LoadSyncServiceConnections()
        {
            refreshCache();
            return _syncServerConnectionsCache;
        }

        /// <summary>
        /// Methods for saving changes in database.
        /// </summary>
        /// <param name="connection">Connection which will be saved in database.</param>
        public void Save(SyncServerConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (connection.SyncServerConnectionId == Guid.Empty|| Context.SyncServerConnections.Where(c => c.SyncServerConnectionId == connection.SyncServerConnectionId).FirstOrDefault() == null)
            {
                if (connection.SyncServerConnectionId == Guid.Empty)
                    connection.SyncServerConnectionId = Guid.NewGuid();

                Context.AddToSyncServerConnections(connection);
            }

            Context.SaveChanges();

            refreshCache();
        }

        /// <summary>
        /// Method which discards changes in Connection, and loads Connection state from database.
        /// </summary>
        /// <param name="connection">Connection which will be reverted.</param>
        public static void Revert(SyncServerConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (connection.SyncServerConnectionId == Guid.Empty) return;

            Context.Refresh(RefreshMode.StoreWins, connection);
        }

        private void refreshCache()
        {
            Context.Refresh(RefreshMode.StoreWins, Context.SyncServerConnections);

            if (_syncServerConnectionsCache == null)
                _syncServerConnectionsCache = new ObservableCollection<SyncServerConnection>();

            _syncServerConnectionsCache.Clear();

            foreach (var connection in Context.SyncServerConnections)
            {
                _syncServerConnectionsCache.Add(connection);
            }
        }
    }
}
