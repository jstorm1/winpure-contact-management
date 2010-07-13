#region References

using System;
using System.Data.SqlServerCe;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServerCe;

#endregion

namespace WinPure.ContactManagement.Common.SyncServiceHelpers
{
    public static class ScopeHelper
    {
        public static bool CheckScope(string connectionString)
        {
            return CheckScope(new SqlCeConnection(connectionString));
        }

        private static bool CheckScope(SqlCeConnection connection)
        {
            try
            {
                SqlCeSyncDescriptionBuilder.GetDescriptionForScope("SyncScope", connection);
            }
            catch (Exception)
            {
                connection.Close();
                return false;
            }
            
            connection.Close();
            return true;
        }

        public static void CreateScope(string connectionString)
        {
            CreateScope(new SqlCeConnection(connectionString));
        }

        private static void CreateScope(SqlCeConnection connection)
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
    }
}

