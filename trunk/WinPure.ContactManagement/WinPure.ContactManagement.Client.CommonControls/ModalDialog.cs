#region References
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

#endregion

namespace WinPure.ContactManagement.Client.CommonControls
{
    public class ModalDialog:PageControl
    {
        public event EventHandler Closed;
        public event EventHandler Opened;

        private bool _dialogResult;

        public virtual bool DialogResult
        {
            get { return _dialogResult; }
            set { _dialogResult = value; }
        }

        public void Close()
        {
            this.Visibility = Visibility.Collapsed;
            if (Closed != null)
                Closed.Invoke(this, null);
        }

        public void Show()
        {
            this.Visibility = Visibility.Visible;
            if (Opened != null)
                Opened.Invoke(this, null);
        }
    }
}
