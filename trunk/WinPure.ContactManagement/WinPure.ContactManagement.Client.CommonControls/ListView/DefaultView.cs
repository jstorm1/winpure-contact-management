using System.Windows;
using System.Windows.Controls;

namespace WinPure.ContactManagement.Client.CommonControls.ListView
{
    public class DefaultView : ViewBase
    {
        public static readonly DependencyProperty ItemContainerStyleProperty =
            ItemsControl.ItemContainerStyleProperty.AddOwner(typeof(DefaultView));

        public static readonly DependencyProperty ItemTemplateProperty =
            ItemsControl.ItemTemplateProperty.AddOwner(typeof(DefaultView));

        public Style ItemContainerStyle
        {
            get { return (Style) GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate) GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        #region DefaultStyleKey

        protected override object DefaultStyleKey
        {
            get { return new ComponentResourceKey(GetType(), "DefaultViewStyle"); }
        }

        #endregion
    }
}