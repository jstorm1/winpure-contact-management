#region References

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

#endregion

namespace WinPure.ContactManagement.Client.CommonControls
{
    [LocalizabilityAttribute(LocalizationCategory.None, Readability = Readability.Unreadable)]
    [ContentProperty("Content")]
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof (ContentPresenter))]
    public class PageControl : Control, IAddChild
    {
        #region Dependecy Properties

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof (string), typeof (PageControl), new UIPropertyMetadata("Page"));


        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof (object), typeof (PageControl),
                                        new PropertyMetadata(null));

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

        [Category("Common Properties")]
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        [Category("Common Properties")]
        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        [Category("Brushes")]
        public SolidColorBrush BorderColor
        {
            get { return (SolidColorBrush) GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        #endregion

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