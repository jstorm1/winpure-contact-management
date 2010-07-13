#region References

using System.IO;
using System.Reflection;

#endregion

namespace WinPure.ContactManagement.Common
{
    public static class Constants
    {
        /// <summary>
        /// Current database name.
        /// </summary>
        public const string DB_NAME = "Data.sdf";

        public const string REMOTE_DB_NAME = "Remote.sdf";
        public const string DOWNLOAD_FOLDER = "Downloads";

        public static readonly string LocalConnectionString = "Data Source=" +
                                                   Path.Combine(GetCurrentDirectoryPath, DB_NAME);

        public static readonly string RemoteConnectionString = "Data Source=" +
                                                        Path.Combine(GetCurrentDirectoryPath, DOWNLOAD_FOLDER, REMOTE_DB_NAME);

        /// <summary>
        /// Returns current Directory path.
        /// </summary>
        public static string GetCurrentDirectoryPath
        {
            get
            {
                string directory =
                    Path.GetDirectoryName(
                        Assembly.GetAssembly(typeof (Constants)).GetName().CodeBase);
                directory = directory.Replace("file:\\", "");
                return directory;
            }
        }
    }
}