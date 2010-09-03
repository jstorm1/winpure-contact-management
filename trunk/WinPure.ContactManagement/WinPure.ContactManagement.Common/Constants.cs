#region References

using System.IO;
using System.Reflection;

#endregion

namespace WinPure.ContactManagement.Common
{
    /// <summary>
    /// Class which contains shared constants.
    /// </summary>
    public static class Constants
    {
        #region Constants

        /// <summary>
        /// Current database name.
        /// </summary>
        public const string DB_NAME = "Data.sdf";

        /// <summary>
        /// Remote database name. 
        /// Remote database - it's a database which was downloaded from remote PC via SyncService.
        /// </summary>
        public const string REMOTE_DB_NAME = "Remote.sdf";

        /// <summary>
        /// Folder name which contains files - which are downloaded from remote PC.
        /// </summary>
        public const string DOWNLOAD_FOLDER = "Downloads";

        public const string SEARCH_QUERY_PATTERN =
            "SELECT * FROM {0} WHERE {1} LIKE @pattern OR Name LIKE @filter OR Name=@pattern";

        #endregion

        #region Fields

        private static readonly string _localConnectionString = "Data Source=" +
                                                  Path.Combine(GetCurrentDirectoryPath, DB_NAME);

        private static readonly string _remoteConnectionString = "Data Source=" +
                                                        Path.Combine(GetCurrentDirectoryPath, DOWNLOAD_FOLDER, REMOTE_DB_NAME); 
        #endregion

        #region Properties
        
        /// <summary>
        /// Endpoint address of current SyncService.
        /// </summary>
        public static string LocalSyncServiceEndpointAddress
        {
            get
            {
                string machineName = System.Net.Dns.GetHostName();
                return string.Format("http://{0}:8000/WinPure/SyncService/", machineName);
            }
        }

        /// <summary>
        /// Returns current Directory path.
        /// </summary>
        public static string GetCurrentDirectoryPath
        {
            get
            {
                string directory =
                    Path.GetDirectoryName(
                        Assembly.GetAssembly(typeof(Constants)).GetName().CodeBase);
                directory = directory.Replace("file:\\", "");
                return directory;
            }
        }

        /// <summary>
        /// Connection string to the local database.
        /// </summary>
        public static string LocalConnectionString
        {
            get { return _localConnectionString; }
        }

        /// <summary>
        /// Connection string to the remote database.
        /// </summary>
        public static string RemoteConnectionString
        {
            get { return _remoteConnectionString; }
        } 

        #endregion
    }
}