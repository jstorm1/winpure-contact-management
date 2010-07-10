#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using WinPure.ContactManagement.Common.Interfaces.SyncService;

#endregion

namespace WinPure.ContactManagement.Client.Services
{
    public class SyncServiceManager
    {
        private ServiceHost _hoster;

        #region Singleton Constructor

        private static SyncServiceManager _instance;

        private SyncServiceManager()
        {
            _hoster = new ServiceHost(typeof (SyncService), new Uri("http://localhost:8000/WinPure/SyncService/"));
        }

        public static SyncServiceManager Current
        {
            get { return _instance ?? (_instance = new SyncServiceManager()); }
        }

        #endregion

        public void RunService()
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