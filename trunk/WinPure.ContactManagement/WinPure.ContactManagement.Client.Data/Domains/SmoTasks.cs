#region References

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo; 

#endregion

namespace WinPure.ContactManagement.Client.Data.Domains
{
    public interface ISmoTasks
    {
        IEnumerable<string> SqlServers { get; }
        List<string> GetDatabases(SqlConnectionString connectionString);
    }

    public class SmoTasks : ISmoTasks
    {
        #region ISmoTasks Members

        public IEnumerable<string> SqlServers
        {
            get
            {
                return SmoApplication
                    .EnumAvailableSqlServers()
                    .AsEnumerable()
                    .Select(r => r["Name"].ToString());
            }
        }

        public List<string> GetDatabases(SqlConnectionString connectionString)
        {
            var databases = new List<string>();

            try
            {
                using (var conn = new SqlConnection(connectionString.WithDatabase("master")))
                {
                    conn.Open();
                    var serverConnection = new ServerConnection(conn);
                    var server = new Server(serverConnection);
                    databases.AddRange(server.Databases.Cast<Database>().Select(database => database.Name));
                }
            }
            catch (Exception)
            {
                return null;
            }
            return databases;
        }

        #endregion
    }
}