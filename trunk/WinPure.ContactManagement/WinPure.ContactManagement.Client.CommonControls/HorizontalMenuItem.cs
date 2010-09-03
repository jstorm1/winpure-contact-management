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

        // Using a DependencyProperty as the backing store for IconGlowColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconGlowColorProperty =
            DependencyProperty.Register("IconGlowColor", typeof (Color), typeof (HorizontalMenuItem),
                                        new UIPropertyMetadata(Colors.Black));


        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof (ImageSource), typeof (HorizontalMenuItem), null);


        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof (string), typeof (HorizontalMenuItem),
                                                new UIPropertyMetadata("Menu Item"));

        // Using a DependencyProperty as the backing store for ImageWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof (double), typeof (HorizontalMenuItem),
                                        new PropertyMetadata(35.0));

        // Using a DependencyProperty as the backing store for ImageHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof (double), typeof (HorizontalMenuItem),
                                        new PropertyMetadata(35.0));

        #endregion

        #region Constructor

        static HorizontalMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (HorizontalMenuItem),
                                                     new FrameworkPropertyMetadata(typeof (HorizontalMenuItem)));
        }

        #endregion

        #region Properties

        [Category("Common Properties")]
        public double ImageHeight
        {
            get { return (double) GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        [Category("Common Properties")]
        public double ImageWidth
        {
            get { return (double) GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        [Category("Brushes")]
        public Color IconGlowColor
        {
            get { return (Color) GetValue(IconGlowColorProperty); }
            set { SetValue(IconGlowColorProperty, value); }
        }

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