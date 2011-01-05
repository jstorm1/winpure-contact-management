#region References

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media; 

#endregion

namespace WinPure.ContactManagement.Client.CommonControls
{
    [TemplatePart(Name = "PART_CheckedImage", Type = typeof(Image))]
    [TemplatePart(Name = "PART_UncheckedImage", Type = typeof(Image))]
    [TemplatePart(Name = "PART_MouseOverImage", Type = typeof(Image))]
    public class ImageRadioButton : RadioButton
    {
        #region Dependency Properties

        // Using a DependencyProperty as the backing store for MouseOverImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverImageProperty =
            DependencyProperty.Register("MouseOverImage", typeof (ImageSource), typeof (ImageRadioButton),
                                        new UIPropertyMetadata(null));


        // Using a DependencyProperty as the backing store for UncheckedImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UncheckedImageProperty =
            DependencyProperty.Register("UncheckedImage", typeof (ImageSource), typeof (ImageRadioButton),
                                        new UIPropertyMetadata(null));


        // Using a DependencyProperty as the backing store for CheckedImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedImageProperty =
            DependencyProperty.Register("CheckedImage", typeof (ImageSource), typeof (ImageRadioButton),
                                        new UIPropertyMetadata(null));


        // Using a DependencyProperty as the backing store for ImagesStretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagesStretchProperty =
            DependencyProperty.Register("ImagesStretch", typeof (Stretch), typeof (ImageRadioButton),
                                        new UIPropertyMetadata(Stretch.Uniform));

        #endregion

        #region Constructor

        static ImageRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ImageRadioButton),
                                                     new FrameworkPropertyMetadata(typeof (ImageRadioButton)));
        }

        #endregion

        #region Properties

        [Category("Images")]
        public Stretch ImagesStretch
        {
            get { return (Stretch) GetValue(ImagesStretchProperty); }
            set { SetValue(ImagesStretchProperty, value); }
        }

        [Category("Images")]
        public ImageSource MouseOverImage
        {
            get { return (ImageSource) GetValue(MouseOverImageProperty); }
            set { SetValue(MouseOverImageProperty, value); }
        }

        [Category("Images")]
        public ImageSource UncheckedImage
        {
            get { return (ImageSource) GetValue(UncheckedImageProperty); }
            set { SetValue(UncheckedImageProperty, value); }
        }

        [Category("Images")]
        public ImageSource CheckedImage
        {
            get { return (ImageSource) GetValue(CheckedImageProperty); }
            set { SetValue(CheckedImageProperty, value); }
        }

        #endregion
    }
}