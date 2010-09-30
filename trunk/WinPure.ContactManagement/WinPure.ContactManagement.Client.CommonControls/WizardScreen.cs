#region References

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

#endregion

namespace WinPure.ContactManagement.Client.CommonControls
{
    /// <summary>
    /// Represents single wizard's page.
    /// </summary>
    public class WizardScreen : ContentControl
    {
        #region Static Constructor

        static WizardScreen()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (WizardScreen),
                                                     new FrameworkPropertyMetadata(typeof (WizardScreen)));
        }

        #endregion

        #region Public Dependency properties

        public static readonly DependencyProperty SideHeaderProperty =
            DependencyProperty.Register("SideHeader", typeof (object), typeof (WizardScreen),
                                        new UIPropertyMetadata(null));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof (object), typeof (WizardScreen), new UIPropertyMetadata(null));

        public static readonly DependencyProperty CanFinishProperty =
            DependencyProperty.Register("CanFinish", typeof (bool), typeof (WizardScreen), new UIPropertyMetadata(false));

        public static readonly DependencyProperty CanNextProperty =
            DependencyProperty.Register("CanNext", typeof (bool), typeof (WizardScreen), new UIPropertyMetadata(true));

        public static readonly DependencyProperty CanPreviousProperty =
            DependencyProperty.Register("CanPrevious", typeof (bool), typeof (WizardScreen),
                                        new UIPropertyMetadata(true));

        public static readonly DependencyProperty CanCancelProperty =
            DependencyProperty.Register("CanCancel", typeof (bool), typeof (WizardScreen), new UIPropertyMetadata(true));

        public static readonly DependencyProperty CanHelpProperty =
            DependencyProperty.Register("CanHelp", typeof (bool), typeof (WizardScreen), new UIPropertyMetadata(false));

        public static readonly DependencyProperty NextStepSizeProperty =
            DependencyProperty.Register("NextStepSize", typeof (int), typeof (WizardScreen),
                                        new UIPropertyMetadata(1, OnNextStepSizeChanged, CoerceNextStepSize));

        public static readonly DependencyProperty PreviousStepSizeProperty =
            DependencyProperty.Register("PreviousStepSize", typeof (int), typeof (WizardScreen),
                                        new UIPropertyMetadata(1, OnPreviousStepSizeChanged, CoercePreviousStepSize));

        // Read-only dependency properties
        private static readonly DependencyPropertyKey CanNavigateNextPropertyKey =
            DependencyProperty.RegisterReadOnly("CanNavigateNext", typeof (bool), typeof (WizardScreen),
                                                new UIPropertyMetadata(true));

        public static readonly DependencyPropertyKey CanNavigatePreviousPropertyKey =
            DependencyProperty.RegisterReadOnly("CanNavigatePrevious", typeof (bool), typeof (WizardScreen),
                                                new UIPropertyMetadata(true));

        public static readonly DependencyProperty IsSelectedProperty =
            Selector.IsSelectedProperty.AddOwner(typeof (WizardScreen),
                                                 new FrameworkPropertyMetadata(false,
                                                                               FrameworkPropertyMetadataOptions.Journal |
                                                                               FrameworkPropertyMetadataOptions.
                                                                                   BindsTwoWayByDefault,
                                                                               OnIsSelectedChanged));

        [Bindable(true), Category("Appearance")]
        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// Gets or sets page's header.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets page's side header.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public object SideHeader
        {
            get { return GetValue(SideHeaderProperty); }
            set { SetValue(SideHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets value indicating whether finish button is enabled for this page. This is a dependency property.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public bool CanFinish
        {
            get { return (bool) GetValue(CanFinishProperty); }
            set { SetValue(CanFinishProperty, value); }
        }

        /// <summary>
        /// Gets or sets value indicating whether next button is enabled for this page. This is a dependency property.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public bool CanNext
        {
            get { return (bool) GetValue(CanNextProperty); }
            set { SetValue(CanNextProperty, value); }
        }

        /// <summary>
        /// Gets or sets value indicating whether previous button is enabled for this page. This is a dependency property.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public bool CanPrevious
        {
            get { return (bool) GetValue(CanPreviousProperty); }
            set { SetValue(CanPreviousProperty, value); }
        }

        /// <summary>
        /// Gets or sets value indicating whether cancel button is enabled for this page. This is a dependency property.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public bool CanCancel
        {
            get { return (bool) GetValue(CanCancelProperty); }
            set { SetValue(CanCancelProperty, value); }
        }

        /// <summary>
        /// Gets or sets value indicating whether help button is enabled for this page. This is a dependency property.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public bool CanHelp
        {
            get { return (bool) GetValue(CanHelpProperty); }
            set { SetValue(CanHelpProperty, value); }
        }

        /// <summary>
        /// Gets value indicating whether it is possible to navigate to the next page. This is a readonly dependency property.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public bool CanNavigateNext
        {
            get { return (bool) GetValue(CanNavigateNextPropertyKey.DependencyProperty); }
            internal set { SetValue(CanNavigateNextPropertyKey, value); }
        }

        /// <summary>
        /// Gets value indicating whether it is possible to navigate to the previous page. This is a readonly dependency property.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        public bool CanNavigatePrevious
        {
            get { return (bool) GetValue(CanNavigatePreviousPropertyKey.DependencyProperty); }
            internal set { SetValue(CanNavigatePreviousPropertyKey, value); }
        }

        [Bindable(true), Category("Appearance")]
        public int PreviousStepSize
        {
            get { return (int) GetValue(PreviousStepSizeProperty); }
            set { SetValue(PreviousStepSizeProperty, value); }
        }

        [Bindable(true), Category("Appearance")]
        public int NextStepSize
        {
            get { return (int) GetValue(NextStepSizeProperty); }
            set { SetValue(NextStepSizeProperty, value); }
        }

        #endregion

        #region Public Routed Events

        public static readonly RoutedEvent PageCloseEvent = EventManager.RegisterRoutedEvent(
            "PageClose", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (WizardScreen));

        public static readonly RoutedEvent PageShowEvent = EventManager.RegisterRoutedEvent(
            "PageShow", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (WizardScreen));

        public static readonly RoutedEvent SelectedEvent = Selector.SelectedEvent.AddOwner(typeof (WizardScreen));

        public static readonly RoutedEvent UnselectedEvent = Selector.UnselectedEvent.AddOwner(typeof (WizardScreen));

        public event RoutedEventHandler PageClose
        {
            add { AddHandler(PageCloseEvent, value); }
            remove { RemoveHandler(PageCloseEvent, value); }
        }

        public event RoutedEventHandler PageShow
        {
            add { AddHandler(PageShowEvent, value); }
            remove { RemoveHandler(PageShowEvent, value); }
        }

        public event RoutedEventHandler Selected
        {
            add { AddHandler(SelectedEvent, value); }
            remove { RemoveHandler(SelectedEvent, value); }
        }

        public event RoutedEventHandler Unselected
        {
            add { AddHandler(UnselectedEvent, value); }
            remove { RemoveHandler(UnselectedEvent, value); }
        }

        #endregion

        #region Protected Internal Helpers

        protected internal virtual void OnPageClose()
        {
            RaiseEvent(new RoutedEventArgs(PageCloseEvent, this));
        }

        protected internal virtual void OnPageShow()
        {
            RaiseEvent(new RoutedEventArgs(PageShowEvent, this));
        }

        #endregion

        #region Private Dependency Property Callbacks

        private static void OnNextStepSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Do nothing
        }

        private static object CoerceNextStepSize(DependencyObject d, object value)
        {
            var stepSize = (int) value;
            if (stepSize < 1)
                return 1;

            return stepSize;
        }

        private static void OnPreviousStepSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Do nothing
        }

        private static object CoercePreviousStepSize(DependencyObject d, object value)
        {
            var stepSize = (int) value;
            if (stepSize < 1)
                return 1;

            return stepSize;
        }

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var container = (WizardScreen) d;
            var newValue = (bool) e.NewValue;

            container.RaiseEvent(newValue
                                     ? new RoutedEventArgs(Selector.SelectedEvent, container)
                                     : new RoutedEventArgs(Selector.UnselectedEvent, container));
        }

        #endregion
    }
}