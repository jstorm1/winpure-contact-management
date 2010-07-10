#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Threading;
using WinPure.ContactManagement.Common.Interfaces.SyncService;

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
            string machineName = System.Net.Dns.GetHostName();
            _hoster = new ServiceHost(typeof (SyncService), new Uri(string.Format("http://{0}:8000/WinPure/SyncService/",machineName)));
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
            var t = new Thread(runService);
            t.Start();
        }

        private void runService()
        {
            _hoster.Open();
        }

        public IEnumerable<EndpointAddress> GetAdderessesOfService()
        {
            var discoverclient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            FindResponse response = discoverclient.Find(new FindCriteria(typeof(ISyncService)));
            return response.Endpoints.Select(e => e.Address);
        }
    }
}