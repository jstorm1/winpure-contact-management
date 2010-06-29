#region References

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#endregion

namespace WinPure.ContactManagement.Client.CommonControls
{
    public class HorizontalMenuItem : Button
    {
        #region Dependency properties

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof (ImageSource), typeof (HorizontalMenuItem), null);


        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof (string), typeof (HorizontalMenuItem),
                                                new UIPropertyMetadata("Menu Item"));

        #endregion

        #region Constructor
        
        static HorizontalMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HorizontalMenuItem),
                                                     new FrameworkPropertyMetadata(typeof(HorizontalMenuItem)));
        } 

        #endregion

        #region Properties

        [Category("Common Properties")]
        public ImageSource Image
        {
            get { return (ImageSource) GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        [Category("Common Properties")]
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion
    }
}