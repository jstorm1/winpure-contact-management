#region References

using System;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data.SqlServerCe;

#endregion

namespace WinPure.ContactManagement.Common.SyncServiceHelpers
{
    /// <summary>
    /// Class which allows to check and create scopes.
    /// </summary>
    public static class ScopeHelper
    {
        #region Check Scope

        /// <summary>
        /// Method which will check scope in database.
        /// </summary>
        /// <param name="connectionString">Connection string to database which will be checked.</param>
        /// <param name="isSqlServer">
        /// If true then connection string will connect with SQL Server database.
        /// If false then connection string will connect with SQL CE database.
        /// </param>
        /// <returns>
        /// If false then scope for that database doesn't exists,
        /// </returns>
        public static bool CheckScope(string connectionString, bool isSqlServer = false)
        {
            return isSqlServer
                       ? checkScope(new SqlConnection(connectionString))
                       : checkScope(new SqlCeConnection(connectionString));
        }

        private static bool checkScope(SqlConnection connection)
        {
            try
            {
                SqlSyncDescriptionBuilder.GetDescriptionForScope("SyncScope", connection);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        private static bool checkScope(SqlCeConnection connection)
        {
            try
            {
                SqlCeSyncDescriptionBuilder.GetDescriptionForScope("SyncScope", connection);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        #endregion

        #region Create Scope

        /// <summary>
        /// Method which will create scope in database.
        /// </summary>
        /// <param name="connectionString">Connection string to database which will contain new scope.</param>
        /// <param name="isSqlServer">
        /// If true then connection string will connect with SQL Server database.
        /// If false then connection string will connect with SQL CE database.
        /// </param>
        public static void CreateScope(string connectionString, bool isSqlServer = false)
        {
            if (!isSqlServer)
            {
                createScope(new SqlCeConnection(connectionString));
            }
            else
            {
                createScope(new SqlConnection(connectionString));
            }
        }

        private static void createScope(SqlConnection connection)
        {
            var scopeDesc = new DbSyncScopeDescription("SyncScope");

            // Definition for Customer.
            DbSyncTableDescription customerDescription =
                SqlSyncDescriptionBuilder.GetDescriptionForTable("Company", connection);

            scopeDesc.Tables.Add(customerDescription);

            customerDescription =
                SqlSyncDescriptionBuilder.GetDescriptionForTable("Contact", connection);
            scopeDesc.Tables.Add(customerDescription);

            // Create a provisioning object for "SyncScope". We specify that
            // base tables should not be created (They already exist in SyncSamplesDb_SqlPeer1),
            // and that all synchronization-related objects should be created in a 
            // database schema named "Sync". If you specify a schema, it must already exist
            // in the database.
            var serverConfig = new SqlSyncScopeProvisioning(scopeDesc);
            serverConfig.SetCreateTableDefault(DbSyncCreationOption.Skip);

            // Configure the scope and change-tracking infrastructure.
            serverConfig.Apply(connection);
            connection.Close();
        }

        private static void createScope(SqlCeConnection connection)
        {
            var scopeDesc = new DbSyncScopeDescription("SyncScope");

            // Definition for Customer.
            DbSyncTableDescription customerDescription =
                SqlCeSyncDescriptionBuilder.GetDescriptionForTable("Company", connection);

            scopeDesc.Tables.Add(customerDescription);

            customerDescription =
                SqlCeSyncDescriptionBuilder.GetDescriptionForTable("Contact", connection);
            scopeDesc.Tables.Add(customerDescription);

            // Create a provisioning object for "SyncScope". We specify that
            // base tables should not be created (They already exist in SyncSamplesDb_SqlPeer1),
            // and that all synchronization-related objects should be created in a 
            // database schema named "Sync". If you specify a schema, it must already exist
            // in the database.
            var serverConfig = new SqlCeSyncScopeProvisioning(scopeDesc);
            serverConfig.SetCreateTableDefault(DbSyncCreationOption.Create);

            // Configure the scope and change-tracking infrastructure.
            serverConfig.Apply(connection);
            connection.Close();
        }

        #endregion
    }
}