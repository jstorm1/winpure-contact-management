#region References

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#endregion

namespace WinPure.ContactManagement.Client.CommonControls
{
    public class PageControl : Control
    {
        #region Dependecy Properties

        // Using a DependencyProperty as the backing store for BorderColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof (SolidColorBrush), typeof (PageControl),
                                        new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        #endregion

        #region Constructor

        static PageControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (PageControl),
                                                     new FrameworkPropertyMetadata(typeof (PageControl)));
        }

        #endregion

        #region Properties

        [Category("Brushes")]
        public SolidColorBrush BorderColor
        {
            get { return (SolidColorBrush) GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        #endregion
    }
}