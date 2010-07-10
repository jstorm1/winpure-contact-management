using WinPure.ContactManagement.Common.Interfaces.SyncService;

namespace WinPure.ContactManagement.Client.Services
{
    public class SyncService:ISyncService
    {
        public string GetMessage(string msg)
        {
            return msg;
        }
    }
}
