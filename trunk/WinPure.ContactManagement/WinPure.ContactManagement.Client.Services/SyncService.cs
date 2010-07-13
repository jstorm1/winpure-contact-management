#region References

using System;
using System.IO;
using WinPure.ContactManagement.Common;
using WinPure.ContactManagement.Common.Interfaces.SyncService; 

#endregion

namespace WinPure.ContactManagement.Client.Services
{
    public class SyncService : ISyncService
    {
        public static event EventHandler DatabaseChanged;

        #region ISyncService Members

        public RemoteFileInfo DownloadFile(DownloadRequest request)
        {
            // get some info about the input file
            string filePath = Path.Combine(Constants.GetCurrentDirectoryPath,Constants.DB_NAME);
            var fileInfo = new FileInfo(filePath);

            // check if exists
            if (!fileInfo.Exists) throw new FileNotFoundException("File not found", Constants.DB_NAME);

            // open stream
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // return result
            var result = new RemoteFileInfo
                             {
                                 FileName = Constants.DB_NAME,
                                 Length = fileInfo.Length,
                                 FileByteStream = stream
                             };
            return result;

            // after returning to the client download starts. Stream remains open and on server and the client reads it, although the execution of this method is completed.
        }

        public void UploadFile(RemoteFileInfo request)
        {
            // create output folder, if does not exist
           //if (!Directory.Exists("Upload")) Directory.CreateDirectory("Upload");

            // kill target file, if already exists
            string filePath = Path.Combine(Constants.GetCurrentDirectoryPath, Constants.DB_NAME);
           //string filePath = Path.Combine("Upload", Constants.DB_NAME);
            if (File.Exists(filePath)) File.Delete(filePath);

            const int chunkSize = 2048;
            var buffer = new byte[chunkSize];

            using (var writeStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
            {
                do
                {
                    // read bytes from input stream
                    int bytesRead = request.FileByteStream.Read(buffer, 0, chunkSize);
                    if (bytesRead == 0) break;

                    // write bytes to output stream
                    writeStream.Write(buffer, 0, bytesRead);
                } while (true);

                writeStream.Close();
            }
            if (DatabaseChanged != null)
            {
                DatabaseChanged.BeginInvoke(null, null, Test, null);
            }
        }

        private void Test(IAsyncResult ar)
        {
            DatabaseChanged.EndInvoke(ar);
        }

        #endregion
    }
}