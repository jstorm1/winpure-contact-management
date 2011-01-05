#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Threading;
using NetFwTypeLib;
using WinPure.ContactManagement.Common;
using WinPure.ContactManagement.Common.Interfaces.SyncService;
using WinPure.ContactManagement.Common.Utils;

#endregion

namespace WinPure.ContactManagement.Client.Services.Managers
{
    /// <summary>
    /// Manager for the synchronization service.
    /// </summary>
    public class SyncServiceManager
    {
        #region Fields

        private readonly ServiceHost _hoster;

        #endregion 

        #region Singleton Constructor

        private static SyncServiceManager _instance;

        private SyncServiceManager()
        {
            _hoster = new ServiceHost(typeof (SyncService), new Uri(Constants.LocalSyncServiceEndpointAddress));
        }

        /// <summary>
        /// Returns current instance.
        /// </summary>
        public static SyncServiceManager Current
        {
            get { return _instance ?? (_instance = new SyncServiceManager()); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Start Sync Service.
        /// </summary>
        public void RunService()
        {
            try
            {
                addExceptionToTheWindowsFirewall();
            }
            finally
            {
                var t = new Thread(runService);
                t.Start();
            }
        }


        /// <summary>
        /// Returns list of addresses of sync service in network.
        /// </summary>
        /// <returns>List of addresses.</returns>
        public static IEnumerable<EndpointAddress> GetAddressesOfService()
        {
            var discoverclient = new DiscoveryClient(new UdpDiscoveryEndpoint());

            FindResponse response = discoverclient.Find(new FindCriteria(typeof (ISyncService)));
            discoverclient.Close();

            return
                response.Endpoints.Where(e => e.Address.ToString().ToUpper() 
                                            != Constants.LocalSyncServiceEndpointAddress.ToUpper())
                                  .Select(e => e.Address);
        }

        private void runService()
        {
            _hoster.Open();
        }

        private static void addExceptionToTheWindowsFirewall()
        {
            var firewallManager = new WindowsFirewallManager();
            firewallManager.Initialize();

            bool isFirewallEnabled = false;
            firewallManager.IsWindowsFirewallOn(ref isFirewallEnabled);

            bool firewallExceptionNotAllowed = false;
            firewallManager.IsExceptionNotAllowed(ref firewallExceptionNotAllowed);

            if (isFirewallEnabled && !firewallExceptionNotAllowed)
            {
                firewallManager.AddPort(8000, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP,
                                        "WinPure Ltd. SyncService Port.");
            }
            firewallManager.Uninitialize();
        }

        #endregion

        #region Destructor

        /// <summary>
        /// Default Destructor.
        /// </summary>
        ~SyncServiceManager()
        {
            _hoster.Close();
        }

        #endregion
    }
}