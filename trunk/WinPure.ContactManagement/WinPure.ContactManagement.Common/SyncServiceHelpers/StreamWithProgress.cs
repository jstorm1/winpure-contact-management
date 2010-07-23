using System;
using System.IO;

namespace WinPure.ContactManagement.Common.SyncServiceHelpers
{
    public class StreamWithProgress : Stream
    {
        private readonly FileStream _file;
        private readonly long _length;

        private long _bytesRead;

        public StreamWithProgress(FileStream file)
        {
            _file = file;
            _length = file.Length;
            _bytesRead = 0;
            if (ProgressChanged != null) ProgressChanged(this, new ProgressChangedEventArgs(_bytesRead, _length));
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override long Position
        {
            get { return _bytesRead; }
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        public double GetProgress()
        {
            return ((double) _bytesRead)/_file.Length;
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int result = _file.Read(buffer, offset, count);
            _bytesRead += result;
            if (ProgressChanged != null) ProgressChanged(this, new ProgressChangedEventArgs(_bytesRead, _length));
            return result;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetLength(long value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #region Nested type: ProgressChangedEventArgs

        public class ProgressChangedEventArgs : EventArgs
        {
            public ProgressChangedEventArgs(long bytesRead, long length)
            {
                BytesRead = bytesRead;
                Length = length;
            }

            public long BytesRead { get; set; }
            public long Length { get; set; }
        }

        #endregion
    }
}