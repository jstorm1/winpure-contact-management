#region References

using System.Windows;
using System.Windows.Controls;

#endregion

namespace WinPure.ContactManagement.Client.CommonControls
{
    public class ProgressControl : Control
    {
        #region Dependency Properties
        
        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ProgressControl),
                                        new UIPropertyMetadata("Loading"));

        // Using a DependencyProperty as the backing store for IsSystemModal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSystemModalProperty =
            DependencyProperty.Register("IsSystemModal", typeof(bool), typeof(ProgressControl),
                                        new UIPropertyMetadata(false)); 
        
        #endregion

        #region Constructor
        
        static ProgressControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressControl),
                                                     new FrameworkPropertyMetadata(typeof(ProgressControl)));
        } 

        #endregion

        #region Properties
        
        public bool IsSystemModal
        {
            get { return (bool)GetValue(IsSystemModalProperty); }
            set { SetValue(IsSystemModalProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        } 

        #endregion
    }
}