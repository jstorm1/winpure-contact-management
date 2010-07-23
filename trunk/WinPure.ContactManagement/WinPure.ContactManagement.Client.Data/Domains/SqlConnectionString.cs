#region References

using System.Data.SqlClient;
using WinPure.ContactManagement.Common.Helpers;

#endregion

namespace WinPure.ContactManagement.Client.Data.Domains
{
    public class SqlConnectionString : PropertyChangedBase
    {
        private readonly SqlConnectionStringBuilder _builder = new SqlConnectionStringBuilder
                                                                   {
                                                                       Pooling = false,
                                                                       IntegratedSecurity = true
                                                                   };

        public SqlConnectionString()
        {
        }

        public SqlConnectionString(string connectionString)
        {
            _builder.ConnectionString = connectionString;
        }

        public string Server
        {
            get { return _builder.DataSource; }
            set
            {
                if (_builder.DataSource == value) return;
                _builder.DataSource = value;
                RaisePropertyChanged("Server");
                RaisePropertyChanged("IsValid");
            }
        }

        public string Database
        {
            get { return _builder.InitialCatalog; }
            set
            {
                if (_builder.InitialCatalog == value) return;
                _builder.InitialCatalog = value;
                RaisePropertyChanged("Database");
                RaisePropertyChanged("IsValid");
            }
        }

        public string UserName
        {
            get { return _builder.UserID; }
            set
            {
                if (_builder.UserID == value) return;
                _builder.UserID = value;
                RaisePropertyChanged("UserName");
                RaisePropertyChanged("IsValid");
            }
        }

        public bool Pooling
        {
            get { return _builder.Pooling; }
            set
            {
                if (_builder.Pooling == value) return;
                _builder.Pooling = value;
                RaisePropertyChanged("Pooling");
                RaisePropertyChanged("IsValid");
            }
        }

        public string Password
        {
            get { return _builder.Password; }
            set
            {
                if (_builder.Password == value) return;
                _builder.Password = value;
                RaisePropertyChanged("Password");
                RaisePropertyChanged("IsValid");
            }
        }

        public bool IntegratedSecurity
        {
            get { return _builder.IntegratedSecurity; }
            set
            {
                if (_builder.IntegratedSecurity == value) return;
                _builder.IntegratedSecurity = value;
                RaisePropertyChanged("IntegratedSecurity");
                RaisePropertyChanged("IsValid");
            }
        }

        public static implicit operator string(SqlConnectionString connectionString)
        {
            return connectionString._builder.ConnectionString;
        }

        public override string ToString()
        {
            return _builder.ConnectionString;
        }

        public SqlConnectionString WithDatabase(string databaseName)
        {
            return new SqlConnectionString
                       {
                           Server = Server,
                           Database = databaseName,
                           IntegratedSecurity = IntegratedSecurity,
                           UserName = UserName,
                           Password = Password,
                           Pooling = Pooling
                       };
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Server) &&
                   !string.IsNullOrEmpty(Database) &&
                   (IntegratedSecurity || (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password)));
        }
    }
}