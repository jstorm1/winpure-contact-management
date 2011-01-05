#region References

using System.Windows;
using System.Windows.Controls; 

#endregion

namespace WinPure.ContactManagement.Client.CommonControls
{
    public class HorizontalMenu : ListBox
    {
        static HorizontalMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (HorizontalMenu),
                                                     new FrameworkPropertyMetadata(typeof (HorizontalMenu)));
        }
    }
}