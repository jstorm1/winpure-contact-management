using System.Windows;
using System.Windows.Controls;

namespace WinPure.ContactManagement.Client.CommonControls.ListView.Views
{
    /// <summary>
    /// Custom Details View for the <see cref="PlainView"/> control.
    /// 
    /// In order to write our own visual tree of a view, we should override its
    /// DefaultStyleKey and ItemContainerDefaultKey. DefaultStyleKey specifies
    /// the style key of <see cref="ListView"/>; ItemContainerDefaultKey specifies the style
    /// key of <see cref="ListViewItem"/>.
    /// </summary>
    public class PlainView : ViewBase
    {
        #region Dependency Properties
        public static readonly DependencyProperty ItemContainerStyleProperty =
    ItemsControl.ItemContainerStyleProperty.AddOwner(typeof(PlainView));

        public static readonly DependencyProperty ItemTemplateProperty =
            ItemsControl.ItemTemplateProperty.AddOwner(typeof(PlainView));

        public static readonly DependencyProperty ItemWidthProperty =
            WrapPanel.ItemWidthProperty.AddOwner(typeof(PlainView));

        public static readonly DependencyProperty ItemHeightProperty =
            WrapPanel.ItemHeightProperty.AddOwner(typeof(PlainView)); 
        #endregion

        #region Properties
        
        public Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        } 

        #endregion

        #region DefaultStyleKey

        protected override object DefaultStyleKey
        {
            get { return new ComponentResourceKey(GetType(), "ImageView"); }
        }

        #endregion
    }
}