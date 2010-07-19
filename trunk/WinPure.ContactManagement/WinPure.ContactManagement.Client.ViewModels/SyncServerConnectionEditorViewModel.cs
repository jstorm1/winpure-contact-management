#region References

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Domains;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.ViewModels.Base;
using WinPure.ContactManagement.Common.Helpers;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class SyncServerConnectionEditorViewModel : ViewModelBase, IDisposable
    {
        #region Fields

        private static ObservableCollection<string> _servers;
        private static readonly object ServersLock = new object();
        private readonly SqlConnectionString _connectionString;

        private readonly ObservableCollection<string> _databases = new ObservableCollection<string>();
        private readonly BackgroundWorker _dbLoader = new BackgroundWorker();
        private readonly ISmoTasks _smoTasks;
        private string _header = "Sql Configuration";
        private string _lastServer;
        private bool _serversLoading;
        private SyncServerConnection _connection;
        private RelayCommand _saveCommand;
        private RelayCommand _cancelCommand;

        #endregion

        #region Constructors

        public SyncServerConnectionEditorViewModel()
            : this(new SmoTasks(), new SqlConnectionString {IntegratedSecurity = true, Pooling = false})
        {
        }

        public SyncServerConnectionEditorViewModel(ISmoTasks smoTasks)
            : this(smoTasks, new SqlConnectionString {IntegratedSecurity = true, Pooling = false})
        {
        }

        public SyncServerConnectionEditorViewModel(ISmoTasks smoTasks, string initialConnectionString)
        {
            _smoTasks = smoTasks;
            _connectionString = new SqlConnectionString(initialConnectionString);

            if (string.IsNullOrEmpty(initialConnectionString))
                _connectionString.IntegratedSecurity = true;

            _connectionString.PropertyChanged += (sender, e) => RaisePropertyChanged("ConnectionString");

            _dbLoader.DoWork += DbLoaderDoWork;
            _dbLoader.RunWorkerCompleted += DbLoaderRunWorkerCompleted;

            _connectionString.PropertyChanged += ConnectionStringPropertyChanged;
        }

        #endregion

        #region Properties

        public ObservableCollection<string> Servers
        {
            get
            {
                lock (ServersLock)
                {
                    if (_servers == null)
                    {
                        _servers = new ObservableCollection<string>();
                        ServersLoading = true;
                        LoadServersAsync();
                    }
                }

                return _servers;
            }
        }

        public ObservableCollection<string> Databases
        {
            get { return _databases; }
        }

        public bool ServersLoading
        {
            get { return _serversLoading; }
            private set
            {
                _serversLoading = value;
                RaisePropertyChanged("ServersLoading");
            }
        }

        public bool DatabasesLoading
        {
            get { return _dbLoader.IsBusy; }
        }

        public SqlConnectionString ConnectionString
        {
            get { return _connectionString; }
        }

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                RaisePropertyChanged("Header");
            }
        }

        public SyncServerConnection Connection
        {
            get { return _connection; }
            set
            {
                if (_connection == value) return;
                _connection = value;
                RaisePropertyChanged("Connection");
            }
        }

        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(Save,canSave)); }
        }

        public RelayCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new RelayCommand(Cancel);
                return _cancelCommand;
            }
        }

        #endregion

        #region Methods
        
        private bool canSave()
        {
            //Check for Design mode.
            if (IsDesignMode) return false;

            Connection.Validate();

            return !Connection.HasErrors && ConnectionString.IsValid();
        }

        private void Cancel()
        {
            SyncServerConnectionsManager.Current.Revert(_connection);
        }

        private void ConnectionStringPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Server" || _dbLoader.IsBusy) return;

            //Server has changed, reload
            _dbLoader.RunWorkerAsync(_connectionString);
            RaisePropertyChanged("DatabasesLoading");
        }


        private void DbLoaderDoWork(object sender, DoWorkEventArgs e)
        {
            var connString = e.Argument as SqlConnectionString;

            //No need to refesh databases if last server is the same as current server
            if (connString == null || _lastServer == connString.Server)
                return;

            _lastServer = connString.Server;

            if (string.IsNullOrEmpty(connString.Server)) return;

            e.Result = _smoTasks.GetDatabases(_connectionString);
        }

        private void DbLoaderRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var databases = e.Result as List<string>;
                if (databases == null)
                {
                    RaisePropertyChanged("DatabasesLoading");
                    return;
                }

                _lastServer = null;

                foreach (string database in databases.OrderBy(d => d))
                {
                    _databases.Add(database);
                }
            }
            else if (ConnectionString.Server != _lastServer)
            {
                _dbLoader.RunWorkerAsync(ConnectionString);
                return;
            }

            RaisePropertyChanged("DatabasesLoading");
        }

        private void LoadServersAsync()
        {
            using (var serverLoader = new BackgroundWorker())
            {
                serverLoader.DoWork += ((sender, e) => e.Result = _smoTasks.SqlServers.OrderBy(r => r).ToArray());

                serverLoader.RunWorkerCompleted += ((sender, e) =>
                                                        {
                                                            // _servers.AddRange((string[]) e.Result);
                                                            foreach (string server in (string[]) e.Result)
                                                            {
                                                                _servers.Add(server);
                                                            }
                                                            ServersLoading = false;
                                                        });

                serverLoader.RunWorkerAsync();
            }
        }

        private void Save()
        {
            _connection.ConnectionString = _connectionString.ToString();

            SyncServerConnectionsManager.Current.Save(_connection);
        }

        #endregion

        #region Dispose methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool cleanUpManaged)
        {
            if (cleanUpManaged)
            {
                GC.SuppressFinalize(this);
                return;
            }

            _dbLoader.Dispose();
        }

        #endregion
    }
}