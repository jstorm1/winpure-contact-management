using System;
using System.ServiceModel;

namespace WinPure.ContactManagement.Common.Interfaces.SyncService
{
    [ServiceContract]
    public interface ISyncService
    {

            [OperationContract]
            void UploadFile(RemoteFileInfo request);

            [OperationContract]
            RemoteFileInfo DownloadFile(DownloadRequest request);


    }

    [MessageContract]
    public class DownloadRequest
    {
        [MessageBodyMember]
        public string FileName;
    }

    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageHeader(MustUnderstand = true)]
        public long Length;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            // close stream when the contract instance is disposed. this ensures that stream is closed when file download is complete, since download procedure is handled by the client and the stream must be closed on server.
            // thanks Bhuddhike! http://blogs.thinktecture.com/buddhike/archive/2007/09/06/414936.aspx
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }
}
