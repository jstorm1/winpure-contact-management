#region References
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows; 
#endregion

namespace WinPure.ContactManagement.Client.Data.Domains.Extensions
{
    public static class SqlConnectionStringExtensions
    {
        public static bool TestConnection(this SqlConnectionString connectionString,
            bool showSuccessDialog, bool showFailureDialog, ref string failureMessage)
        {
            if (string.IsNullOrEmpty(connectionString.Server))
            {
                if (showFailureDialog)
                    MessageBox.Show("Server name not specified", "Connection failed", MessageBoxButton.OK,
                         MessageBoxImage.Asterisk);

                return false;
            }

            if (string.IsNullOrEmpty(connectionString.Database))
            {
                if (showFailureDialog)
                    MessageBox.Show("Initial Catalog not specified", "Connection failed", MessageBoxButton.OK,
                         MessageBoxImage.Asterisk);

                return false;
            }

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (SqlException ex)
                {
                    failureMessage = ex.Message;

                    if (showFailureDialog)
                    {
                        MessageBox.Show("The following error occured:\r\n" + ex.Message, "Connection failed", MessageBoxButton.OK,
                         MessageBoxImage.Asterisk);
                    }

                    return false;
                }

                if (showSuccessDialog)
                    MessageBox.Show("Successfully connected to the databse", "Connection successful", MessageBoxButton.OK,
                         MessageBoxImage.Asterisk);

                return true;
            }
        }
    }
}
