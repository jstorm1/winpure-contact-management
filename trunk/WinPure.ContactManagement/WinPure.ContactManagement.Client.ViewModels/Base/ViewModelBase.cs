#region References

using System.ComponentModel;
using System.Windows;
using WinPure.ContactManagement.Common;
using WinPure.ContactManagement.Common.Helpers;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels.Base
{
    public class ViewModelBase : PropertyChangedBase
    {
        protected bool IsDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(new DependencyObject());
            }
        }
    }
}