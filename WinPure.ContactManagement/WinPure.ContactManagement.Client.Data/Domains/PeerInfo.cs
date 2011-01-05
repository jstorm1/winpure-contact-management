#region References

using System.ServiceModel;
using WinPure.ContactManagement.Common;
using WinPure.ContactManagement.Common.Helpers;

#endregion

namespace WinPure.ContactManagement.Client.Data.Domains
{
    public class PeerInfo : PropertyChangedBase
    {
        public EndpointAddress Address { get; set; }
        public string HostName { get; set; }
        public bool IsSyncAvailable { get; set; }
    }
}