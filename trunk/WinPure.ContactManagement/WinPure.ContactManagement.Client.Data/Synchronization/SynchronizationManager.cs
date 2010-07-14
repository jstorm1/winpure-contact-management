#region References

using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.ServiceModel;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data.SqlServerCe;
using WinPure.ContactManagement.Client.Data.Managers;
using WinPure.ContactManagement.Client.Data.SyncService;
using WinPure.ContactManagement.Client.Services;
using WinPure.ContactManagement.Common;
using WinPure.ContactManagement.Common.SyncServiceHelpers;

#endregion

namespace WinPure.ContactManagement.Client.Data.Synchronization
{
    public class SynchronizationManager
    {
        public static IEnumerable<EndpointAddress> GetAddressesOfSyncService()
        {
            return SyncServiceManager.Current.GetAddressesOfService();
        }

        public static void Synchronize(EndpointAddress remoteAddress)
        {
            SyncServiceClient serviceClient = ServiceProxy(remoteAddress);

            downloadDatabase(serviceClient);

            using (var localDbConnection = new SqlCeConnection(Constants.LocalConnectionString))
            {
                using (var remoteDbConnection = new SqlCeConnection(Constants.RemoteConnectionString))
                {
                    var syncOrchestrator = new SyncOrchestrator
                                               {
                                                   Direction = SyncDirectionOrder.DownloadAndUpload,
                                                   RemoteProvider =
                                                       new SqlCeSyncProvider("SyncScope", remoteDbConnection),
                                                   LocalProvider = new SqlCeSyncProvider("SyncScope", localDbConnection)
                                               };

                    syncOrchestrator.Synchronize();

                    uploadDatabase(serviceClient);
                }
            }

            ContactsManager.Current.RefreshCache();
        }

        #region Service Methods

        public static SyncServiceClient ServiceProxy(EndpointAddress address)
        {
            var binding = new BasicHttpBinding
                              {
                                  Security = { Mode = BasicHttpSecurityMode.None },
                                  MaxReceivedMessageSize = 10067108864,
                                  MessageEncoding = WSMessageEncoding.Mtom,
                                  TransferMode = TransferMode.Streamed
                              };

            return new SyncServiceClient(binding, address);
        }

        private static void uploadDatabase(SyncServiceClient serviceClient)
        {
            string dbPath = Path.Combine(Constants.GetCurrentDirectoryPath, Constants.DOWNLOAD_FOLDER,
                                         Constants.REMOTE_DB_NAME);

            // get some info about the input file
            var fileInfo = new FileInfo(dbPath);

            FileStream stream = null;
            try
            {
                // open input stream
                stream = new FileStream(dbPath, FileMode.Open, FileAccess.Read);

                using (var uploadStreamWithProgress = new StreamWithProgress(stream))
                {
                    stream = null;
                    uploadStreamWithProgress.ProgressChanged += uploadStreamWithProgress_ProgressChanged;

                    // upload file
                    serviceClient.UploadFile(fileInfo.Name, fileInfo.Length, uploadStreamWithProgress);
                }
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        private static void downloadDatabase(SyncServiceClient serviceClient)
        {
            // create output folder, if does not exist
            if (!Directory.Exists(Constants.DOWNLOAD_FOLDER)) Directory.CreateDirectory(Constants.DOWNLOAD_FOLDER);

            // kill target file, if already exists
            string filePath = Path.Combine(Constants.DOWNLOAD_FOLDER, Constants.REMOTE_DB_NAME);
            if (File.Exists(filePath)) File.Delete(filePath);

            // get stream from server
            Stream inputStream;
            string fileName = "";
            long length = serviceClient.DownloadFile(ref fileName, out inputStream);

            // write server stream to disk
            using (var writeStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
            {
                const int chunkSize = 2048;
                var buffer = new byte[chunkSize];

                do
                {
                    // read bytes from input stream
                    int bytesRead = inputStream.Read(buffer, 0, chunkSize);
                    if (bytesRead == 0) break;

                    // write bytes to output stream
                    writeStream.Write(buffer, 0, bytesRead);

                    // report progress from time to time
                    //progressBar1.Value = (int)(writeStream.Position * 100 / length);
                } while (true);
            }

            // close service client
            inputStream.Dispose();
            // serviceClient.Close();
        }

        private static void uploadStreamWithProgress_ProgressChanged(object sender,
                                                                     StreamWithProgress.ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}