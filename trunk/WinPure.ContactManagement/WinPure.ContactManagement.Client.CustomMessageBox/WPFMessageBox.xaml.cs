using System;
using System.Windows;
using BlogsPrajeesh.BlogSpot.WPFControls;

namespace WinPure.ContactManagement.Client.CustomMessageBox
{
    /// <summary>
    /// Interaction logic for WPFMessageBox.xaml
    /// </summary>
    public partial class WPFMessageBox : Window
    {
        [ThreadStatic] private static WPFMessageBox ___MessageBox;

        public WPFMessageBox()
        {
            InitializeComponent();
        }

        public WPFMessageBoxResult Result { get; set; }

        public static WPFMessageBoxResult Show(string message)
        {
            return Show(string.Empty, message, string.Empty, WPFMessageBoxButtons.OK, WPFMessageBoxImage.Default);
        }

        public static WPFMessageBoxResult Show(string title, string message)
        {
            return Show(title, message, string.Empty, WPFMessageBoxButtons.OK, WPFMessageBoxImage.Default);
        }

        public static WPFMessageBoxResult Show(string title, string message, string details)
        {
            return Show(title, message, details, WPFMessageBoxButtons.OK, WPFMessageBoxImage.Default);
        }

        public static WPFMessageBoxResult Show(string title, string message, WPFMessageBoxButtons buttonOption)
        {
            return Show(title, message, string.Empty, buttonOption, WPFMessageBoxImage.Default);
        }

        public static WPFMessageBoxResult Show(string title, string message, string details,
                                               WPFMessageBoxButtons buttonOption)
        {
            return Show(title, message, details, buttonOption, WPFMessageBoxImage.Default);
        }

        public static WPFMessageBoxResult Show(string title, string message, WPFMessageBoxImage image)
        {
            return Show(title, message, string.Empty, WPFMessageBoxButtons.OK, image);
        }

        public static WPFMessageBoxResult Show(string title, string message, string details, WPFMessageBoxImage image)
        {
            return Show(title, message, details, WPFMessageBoxButtons.OK, image);
        }

        public static WPFMessageBoxResult Show(string title, string message, WPFMessageBoxButtons buttonOption,
                                               WPFMessageBoxImage image)
        {
            return Show(title, message, string.Empty, buttonOption, image);
        }

        public static WPFMessageBoxResult Show(string title, string message, string details,
                                               WPFMessageBoxButtons buttonOption, WPFMessageBoxImage image)
        {
            ___MessageBox = new WPFMessageBox();
            var __ViewModel = new MessageBoxViewModel(___MessageBox, title, message, details, buttonOption, image);
            ___MessageBox.DataContext = __ViewModel;
            ___MessageBox.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ___MessageBox.ShowDialog();
            return ___MessageBox.Result;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }
    }
}