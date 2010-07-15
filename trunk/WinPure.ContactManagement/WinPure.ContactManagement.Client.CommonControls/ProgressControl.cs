using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WinPure.ContactManagement.Common.Helpers;

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

        private static Grid _mainWindowFrontGrid;

        static ProgressControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ProgressControl),
                                                     new FrameworkPropertyMetadata(typeof (ProgressControl)));
        }

        public ProgressControl()
        {
            _mainWindowFrontGrid = ((DependencyObject)Application.Current.MainWindow.Content).GetChildObjects<Grid>("FrontGrid").FirstOrDefault();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.Name != "IsVisible" || !IsSystemModal || _mainWindowFrontGrid == null) return;
            
            var visibility = (Visibility)e.NewValue;
                
            if(visibility == Visibility.Visible)
            {
                ((Grid) Parent).Children.Remove(this);

                _mainWindowFrontGrid.Children.Clear();
                _mainWindowFrontGrid.Children.Add(this);
            }
            else
            {
                _mainWindowFrontGrid.Children.Clear();
            }
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