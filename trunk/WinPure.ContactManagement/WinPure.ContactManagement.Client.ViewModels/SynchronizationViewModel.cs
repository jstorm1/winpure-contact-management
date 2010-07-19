#region References

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.Data.Domains;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Data.Synchronization;
using WinPure.ContactManagement.Client.ViewModels.Base;
using WinPure.ContactManagement.Common;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class SynchronizationViewModel : ViewModelBase, IDisposable
    {
        #region Fields

        private readonly BackgroundWorker _getEndpointsBackgroundWorker;
        private readonly BackgroundWorker _synchronizationWorker;
        private readonly BackgroundWorker _sqlSynchronizationWorker;
        private string _busyMessage;
        private bool _isBusy;
        private SynchronisedObservableCollection<PeerInfo> _peersInfo;
        private RelayCommand _refreshPeersCommand;
        private PeerInfo _selectedPeerInfo;
        private SyncServerConnection _selectedSyncServerConnection;
        private ObservableCollection<SyncServerConnection> _syncServerConnections;
        private RelayCommand _synchronizeCommand;
        private RelayCommand _sqlSynchronizeCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public SynchronizationViewModel()
        {
            _getEndpointsBackgroundWorker = new BackgroundWorker();
            _getEndpointsBackgroundWorker.DoWork += onGetingEndpointsDoWork;
            _getEndpointsBackgroundWorker.RunWorkerCompleted += onGetingEndpointsCompleted;

            _synchronizationWorker = new BackgroundWorker();
            _synchronizationWorker.DoWork += onSynchronizationDoWork;
            _synchronizationWorker.RunWorkerCompleted += onSynchronizationCompleted;

            _sqlSynchronizationWorker = new BackgroundWorker();
            _sqlSynchronizationWorker.DoWork += onSqlSynchronizationDoWork;
            _sqlSynchronizationWorker.RunWorkerCompleted += onSqlSynchronizationCompleted;

            refreshPeersInfo();

            SyncServerConnections = SyncServerConnectionsManager.Current.LoadSyncServiceConnections();
        }

        #endregion

        #region Properties

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        public string BusyMessage
        {
            get { return _busyMessage; }
            set
            {
                if (_busyMessage == value) return;
                _busyMessage = value;
                RaisePropertyChanged("BusyMessage");
            }
        }

        public SynchronisedObservableCollection<PeerInfo> PeersInfo
        {
            get { return _peersInfo; }
            set
            {
                if (_peersInfo == value) return;
                _peersInfo = value;
                RaisePropertyChanged("PeersInfo");
            }
        }

        public RelayCommand RefreshPeersCommand
        {
            get { return _refreshPeersCommand ?? (_refreshPeersCommand = new RelayCommand(refreshPeersInfo)); }
        }

        public PeerInfo SelectedPeerInfo
        {
            get { return _selectedPeerInfo; }
            set
            {
                if (_selectedPeerInfo == value) return;
                _selectedPeerInfo = value;
                RaisePropertyChanged("SelectedPeerInfo");
            }
        }

        public RelayCommand SynchronizeCommand
        {
            get { return _synchronizeCommand ?? (_synchronizeCommand = new RelayCommand(synchronize, canSynchronize)); }
        }

        public ObservableCollection<SyncServerConnection> SyncServerConnections
        {
            get { return _syncServerConnections; }
            set
            {
                if (_syncServerConnections == value) return;
                _syncServerConnections = value;
                RaisePropertyChanged("SyncServerConnections");
            }
        }

        public SyncServerConnection SelectedSyncServerConnection
        {
            get { return _selectedSyncServerConnection; }
            set
            {
                if (_selectedSyncServerConnection == value) return;
                _selectedSyncServerConnection = value;
                RaisePropertyChanged("SelectedSyncServerConnection");
            }
        }

        public RelayCommand SqlSynchronizeCommand
        {
            get { return _sqlSynchronizeCommand ?? (_sqlSynchronizeCommand = new RelayCommand(sqlSynchronize, canSqlSynchronize)); }
        }

        #endregion

        private bool canSqlSynchronize()
        {
            return SelectedSyncServerConnection != null;
        }

        private bool canSynchronize()
        {
            return SelectedPeerInfo != null;
        }

        private void refreshPeersInfo()
        {
            IsBusy = true;
            BusyMessage = "Searching...";
            _getEndpointsBackgroundWorker.RunWorkerAsync();
        }

        private void sqlSynchronize()
        {
            IsBusy = true;
            BusyMessage = "Synchronization...";
            _sqlSynchronizationWorker.RunWorkerAsync();
        }


        private void onSqlSynchronizationCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SynchronizationManager.Synchronize(SelectedSyncServerConnection.ConnectionString);
        }

        private void onSqlSynchronizationDoWork(object sender, DoWorkEventArgs e)
        {
            IsBusy = false;
        }

        private void synchronize()
        {
            IsBusy = true;
            BusyMessage = "Synchronization...";
            _synchronizationWorker.RunWorkerAsync();
        }

        private void onGetingEndpointsCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object addresses = e.Result;

            if (PeersInfo == null)
                PeersInfo =
                    new SynchronisedObservableCollection<PeerInfo>(new ObservableCollection<PeerInfo>());

            PeersInfo.Clear();

            foreach (EndpointAddress endpointAddress in (IEnumerable<EndpointAddress>) addresses)
            {
                var peerInfo = new PeerInfo
                                   {
                                       Address = endpointAddress,
                                       HostName = endpointAddress.Uri.Host.ToUpper(),
                                       IsSyncAvailable = false
                                   };
                PeersInfo.Add(peerInfo);
            }

            IsBusy = false;
        }

        private static void onGetingEndpointsDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = SynchronizationManager.GetAddressesOfSyncService();
        }

        private void onSynchronizationCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
        }

        private void onSynchronizationDoWork(object sender, DoWorkEventArgs e)
        {
            SynchronizationManager.Synchronize(SelectedPeerInfo.Address);
        }

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

            _getEndpointsBackgroundWorker.Dispose();
            _synchronizationWorker.Dispose();
            _sqlSynchronizationWorker.Dispose();
        }

        #endregion
    }
}