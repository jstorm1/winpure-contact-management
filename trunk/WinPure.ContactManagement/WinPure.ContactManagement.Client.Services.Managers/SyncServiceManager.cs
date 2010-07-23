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

namespace WinPure.ContactManagement.Client.Services
{
    public class SyncServiceManager
    {
        private readonly ServiceHost _hoster;

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

        public void RunService()
        {
            try
            {
                addExceptionToTheWindowsFirewall();
            }
            finally
            {
            }

            var t = new Thread(runService);
            t.Start();
        }

        private void runService()
        {
            _hoster.Open();
        }

        public IEnumerable<EndpointAddress> GetAddressesOfService()
        {
            var discoverclient = new DiscoveryClient(new UdpDiscoveryEndpoint());

            FindResponse response = discoverclient.Find(new FindCriteria(typeof (ISyncService)));

            discoverclient.Close();

            return
                response.Endpoints.Where(e => e.Address.ToString().ToUpper() != Constants.LocalSyncServiceEndpointAddress.ToUpper()).Select(
                    e => e.Address);
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

        ~SyncServiceManager()
        {
            _hoster.Close();
        }
    }
}