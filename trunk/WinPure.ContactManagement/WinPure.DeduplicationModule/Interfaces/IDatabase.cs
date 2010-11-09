using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WinPure.DeduplicationModule.Interfaces
{
    public enum Dbms { MsAccess, MsSQL };

    interface IDatabase
    {
        DataTable LoadContentFromDatabase(Dbms   dbms,
                                          string databaseName,
                                          string login,
                                          string password,
                                          string tableName);

        DataTable LoadContentFromMsAccess(string databaseName,
                                          string login,
                                          string password,
                                          string tableName);

        DataTable LoadContentFromMsSQL(string databaseName,
                                       string login,
                                       string password,
                                       string tableName);
    }
}
