#region References

using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup; 

#endregion

namespace WinPure.ContactManagement.Client.CommonControls
{
    [LocalizabilityAttribute(LocalizationCategory.None, Readability = Readability.Unreadable)]
    [ContentProperty("Content")]
    public class WizardScreen : Control, IAddChild
    {
        // Using a DependencyProperty as the backing store for IsReadyToContinue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadyToContinueProperty =
            DependencyProperty.Register("IsReadyToContinue", typeof(bool), typeof(WizardScreen),
                                        new UIPropertyMetadata(false, onIsReadyToContinuePropertyChanged));

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof (object), typeof (WizardScreen), new UIPropertyMetadata(null));


        // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register("Owner", typeof (WizardControl), typeof (WizardScreen),
                                        new UIPropertyMetadata(null));


        static WizardScreen()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WizardScreen),
                             new FrameworkPropertyMetadata(typeof(WizardScreen)));
        }

        public WizardScreen(WizardControl owner = null)
        {
            Owner = owner;
        }

        public WizardControl Owner
        {
            get { return (WizardControl) GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public bool IsReadyToContinue
        {
            get { return (bool)GetValue(IsReadyToContinueProperty); }
            set { SetValue(IsReadyToContinueProperty, value); }
        }

        private static void onIsReadyToContinuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var screen = d as WizardScreen;
            if (screen == null) return;

            if (screen.Owner == null) return;
            screen.Owner.NextButtonVisibility = (bool)e.NewValue != true ? Visibility.Hidden : Visibility.Visible;
        }

        #region Implementation of IAddChild

        /// <summary>
        /// Adds a child object. 
        /// </summary>
        /// <param name="value">The child object to add.</param>
        public void AddChild(object value)
        {
            Content = value;
        }

        /// <summary>
        /// Adds the text content of a node to the object. 
        /// </summary>
        /// <param name="text">The text to add to the object.</param>
        public void AddText(string text)
        {
            Content = new TextBox {Text = text};
        }

        #endregion
    }
}