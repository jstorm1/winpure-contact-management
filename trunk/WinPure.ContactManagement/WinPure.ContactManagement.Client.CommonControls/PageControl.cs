#region References

using System;
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
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof (ContentPresenter))]
    public class PageControl : Control, IAddChild
    {
        #region Fields

        private ModalDialog _modalDialog;
        private Canvas _modalOwner;

        #endregion

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

        public ModalDialog ModalDialog
        {
            get { return _modalDialog; }
            set
            {
                _modalDialog = value;
                if (_modalDialog.Visibility == Visibility.Visible)
                {
                    ShowModalPage(_modalDialog);
                }
                else
                {
                    _modalDialog.Opened += onModalDialogOpened;
                }
            }
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

        #region Methods

        public void ShowModalPage(ModalDialog dialog)
        {
            _modalOwner.Children.Clear();
            _modalOwner.Children.Add(dialog);

            //Sets position of modal dialog at center of _modalOwner Canvas.
            Canvas.SetLeft(dialog, (ActualWidth - dialog.Width)/2);
            Canvas.SetTop(dialog, (ActualHeight - dialog.Height)/2);

            VisualStateManager.GoToState(this, "ShowModalDialog", true);
            dialog.Closed += onModalDialogClosed;
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or 
        /// internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _modalOwner = (Canvas) GetTemplateChild("ModalDialogOwner");
        }

        #endregion

        #region Events Handlers

        private void onModalDialogOpened(object sender, EventArgs e)
        {
            ShowModalPage((ModalDialog) sender);
        }


        private void onModalDialogClosed(object sender, EventArgs e)
        {
            _modalOwner.Children.Clear();
            VisualStateManager.GoToState(this, "Normal", true);
        }

        #endregion
    }
}