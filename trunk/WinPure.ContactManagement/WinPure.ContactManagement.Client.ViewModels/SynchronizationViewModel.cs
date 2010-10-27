﻿#region References

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel;
using GalaSoft.MvvmLight.Command;
using WinPure.ContactManagement.Client.CustomMessageBox;
using WinPure.ContactManagement.Client.Data.Domains;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Data.Synchronization;
using WinPure.ContactManagement.Client.Localization;
using WinPure.ContactManagement.Client.ViewModels.Base;
using WinPure.ContactManagement.Common.Helpers;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels
{
    public class SynchronizationViewModel : ViewModelBase, IDisposable
    {
        #region Fields

        private readonly BackgroundWorker _getEndpointsBackgroundWorker;
        private readonly BackgroundWorker _sqlSynchronizationWorker;
        private readonly BackgroundWorker _synchronizationWorker;
        private string _busyMessage;
        private RelayCommand _deleteCommand;
        private bool _isBusy;
        private SynchronizedObservableCollection<PeerInfo> _peersInfo;
        private RelayCommand _refreshPeersCommand;
        private PeerInfo _selectedPeerInfo;
        private SyncServerConnection _selectedSyncServerConnection;
        private int _sqlConnectionsCount;
        private RelayCommand _sqlSynchronizeCommand;
        private ObservableCollection<SyncServerConnection> _syncServerConnections;
        private RelayCommand _synchronizeCommand;

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

        public SynchronizedObservableCollection<PeerInfo> PeersInfo
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

        public ObservableCollection<SyncServerConnection> SyncServerConnections
        {
            get { return _syncServerConnections; }
            set
            {
                if (_syncServerConnections == value) return;
                _syncServerConnections = value;
                RaisePropertyChanged("SyncServerConnections");

                if (_syncServerConnections != null)
                {
                    _syncServerConnections.CollectionChanged +=
                        delegate { RaisePropertyChanged("SqlConnectionsCount"); };
                }
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

        public int SqlConnectionsCount
        {
            get { return _sqlConnectionsCount; }
            set
            {
                if (_sqlConnectionsCount == value) return;
                _sqlConnectionsCount = value;
                RaisePropertyChanged("SqlConnectionsCount");
            }
        }

        #region Commands
        
        public RelayCommand SynchronizeCommand
        {
            get { return _synchronizeCommand ?? (_synchronizeCommand = new RelayCommand(synchronize, canSynchronize)); }
        }

        public RelayCommand SqlSynchronizeCommand
        {
            get
            {
                return _sqlSynchronizeCommand ??
                       (_sqlSynchronizeCommand = new RelayCommand(sqlSynchronize, canSqlSynchronize));
            }
        }

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(delete)); }
        }

        #endregion

        #endregion

        #region Mehods

        private void delete()
        {
            WPFMessageBoxResult result = WPFMessageBox.Show(LanguageDictionary.CurrentDictionary.Translate<string>("Messages.DeleteConnection", "Title"),
                                                            LanguageDictionary.CurrentDictionary.Translate<string>("Messages.DeleteConnection", "Message"),
                                                            WPFMessageBoxButtons.YesNo,
                                                            WPFMessageBoxImage.Question);
            if (result != WPFMessageBoxResult.Yes) return;

            SyncServerConnectionsManager.Current.Delete(_selectedSyncServerConnection);
        }

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
            BusyMessage = LanguageDictionary.CurrentDictionary.Translate<string>("Messages.BusyMessage.Searching", "Message");
            _getEndpointsBackgroundWorker.RunWorkerAsync();
        }

        private void sqlSynchronize()
        {
            IsBusy = true;
            BusyMessage = LanguageDictionary.CurrentDictionary.Translate<string>("Messages.BusyMessage.Synchronization", "Message");
            _sqlSynchronizationWorker.RunWorkerAsync();
        }


        private void onSqlSynchronizationCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsBusy = false;
        }

        private void onSqlSynchronizationDoWork(object sender, DoWorkEventArgs e)
        {
            SynchronizationManager.Synchronize(SelectedSyncServerConnection.ConnectionString);
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
                    new SynchronizedObservableCollection<PeerInfo>(new ObservableCollection<PeerInfo>());

            PeersInfo.Clear();

            foreach (EndpointAddress endpointAddress in (IEnumerable<EndpointAddress>)addresses)
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

            _getEndpointsBackgroundWorker.Dispose();
            _synchronizationWorker.Dispose();
            _sqlSynchronizationWorker.Dispose();
        }

        #endregion
    }
}