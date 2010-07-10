using System.ServiceModel;

namespace WinPure.ContactManagement.Common.Interfaces.SyncService
{
    [ServiceContract]
    public interface ISyncService
    {
        [OperationContract]
        string GetMessage(string msg);
    }
}
