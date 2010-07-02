#region References

using System.ComponentModel;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this,
                        new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}