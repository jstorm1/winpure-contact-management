using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WinPure.ContactManagement.Common.Helpers;
using WinPure.ContactManagement.Common.Interfaces.Windows;

namespace WinPure.ContactManagement.Client.CommonControls
{
    public class ProgressControl : Control
    {
        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof (string), typeof (ProgressControl),
                                        new UIPropertyMetadata("Loading"));

        // Using a DependencyProperty as the backing store for IsSystemModal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSystemModalProperty = 
    DependencyProperty.Register("IsSystemModal", typeof(bool), typeof(ProgressControl), new UIPropertyMetadata(false));

        static ProgressControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ProgressControl),
                                                     new FrameworkPropertyMetadata(typeof (ProgressControl)));
        }

        public ProgressControl()
        {
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            var shellWindow = Application.Current.MainWindow as IShellWindow;

            if (e.Property.Name != "IsVisible" || !IsSystemModal || shellWindow == null) return;
            
            var visibility = Visibility;
                
            shellWindow.ModalContent = visibility == Visibility.Visible ? this : null;
        }


        public bool IsSystemModal
        {
            get { return (bool) GetValue(IsSystemModalProperty); }
            set { SetValue(IsSystemModalProperty, value); }
        }

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}