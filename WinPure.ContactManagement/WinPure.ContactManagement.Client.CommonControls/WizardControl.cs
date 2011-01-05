using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WinPure.ContactManagement.Client.CommonControls
{
    /// <summary>
    /// Represents a wizard control.
    /// </summary>
    [TemplatePart(Name = PART_Finish, Type = typeof (ButtonBase))]
    [TemplatePart(Name = PART_Next, Type = typeof (ButtonBase))]
    [TemplatePart(Name = PART_Previous, Type = typeof (ButtonBase))]
    [TemplatePart(Name = PART_Cancel, Type = typeof (ButtonBase))]
    [TemplatePart(Name = PART_Help, Type = typeof (ButtonBase))]
    public class Wizard : Selector
    {
        #region Public Resource Keys

        public static ResourceKey HeaderPanelBorderResourceKey =
            new ComponentResourceKey(typeof (Wizard), "HeaderPanelBorderResourceKey");

        public static ResourceKey SideHeaderPanelBorderResourceKey =
            new ComponentResourceKey(typeof (Wizard), "SideHeaderPanelBorderResourceKey");

        public static ResourceKey ContentPanelBorderResourceKey =
            new ComponentResourceKey(typeof (Wizard), "ContentPanelBorderResourceKey");

        public static ResourceKey NavigationPanelBorderResourceKey =
            new ComponentResourceKey(typeof (Wizard), "NavigationPanelBorderResourceKey");

        public static ResourceKey NavigationButtonResourceKey =
            new ComponentResourceKey(typeof (Wizard), "NavigationButtonResourceKey");

        #endregion

        #region Private Constants

        private const string PART_Finish = "PART_Finish";
        private const string PART_Next = "PART_Next";
        private const string PART_Previous = "PART_Previous";
        private const string PART_Cancel = "PART_Cancel";
        private const string PART_Help = "PART_Help";

        #endregion

        #region Static Constructor

        static Wizard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (Wizard),
                                                     new FrameworkPropertyMetadata(typeof (Wizard)));
        }

        #endregion

        #region Public Routed Events

        public static readonly RoutedEvent HelpClickEvent = EventManager.RegisterRoutedEvent(
            "HelpClick", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (Wizard));

        public static readonly RoutedEvent CancelClickEvent = EventManager.RegisterRoutedEvent(
            "CancelClick", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (Wizard));

        public static readonly RoutedEvent PreviousClickEvent = EventManager.RegisterRoutedEvent(
            "PreviousClick", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (Wizard));

        public static readonly RoutedEvent NextClickEvent = EventManager.RegisterRoutedEvent(
            "NextClick", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (Wizard));

        public static readonly RoutedEvent FinishClickEvent = EventManager.RegisterRoutedEvent(
            "FinishClick", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (Wizard));

        public event RoutedEventHandler HelpClick
        {
            add { AddHandler(HelpClickEvent, value); }
            remove { RemoveHandler(HelpClickEvent, value); }
        }

        public event RoutedEventHandler CancelClick
        {
            add { AddHandler(CancelClickEvent, value); }
            remove { RemoveHandler(CancelClickEvent, value); }
        }

        public event RoutedEventHandler PreviousClick
        {
            add { AddHandler(PreviousClickEvent, value); }
            remove { RemoveHandler(PreviousClickEvent, value); }
        }

        public event RoutedEventHandler NextClick
        {
            add { AddHandler(NextClickEvent, value); }
            remove { RemoveHandler(NextClickEvent, value); }
        }

        public event RoutedEventHandler FinishClick
        {
            add { AddHandler(FinishClickEvent, value); }
            remove { RemoveHandler(FinishClickEvent, value); }
        }

        #endregion

        #region Public/Protected Overrides

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            // If the wizard contains pages, setup defaults
            if (Items.Count > 0)
                SelectedIndex = 0;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var button = GetTemplateChild(PART_Finish) as ButtonBase;
            if (button != null)
                button.Click += (OnFinishedClicked);

            button = GetTemplateChild(PART_Next) as ButtonBase;
            if (button != null)
                button.Click += (OnNextClicked);

            button = GetTemplateChild(PART_Previous) as ButtonBase;
            if (button != null)
                button.Click += (OnPreviousClicked);

            button = GetTemplateChild(PART_Cancel) as ButtonBase;
            if (button != null)
                button.Click += (OnCancelClicked);

            button = GetTemplateChild(PART_Help) as ButtonBase;
            if (button != null)
                button.Click += (OnHelpClicked);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new WizardScreen();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is WizardScreen;
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (DesignerProperties.GetIsInDesignMode(this)) return;

            WizardScreen page = e.AddedItems.Count == 1 ? (WizardScreen) e.AddedItems[0] : null;
            WizardScreen oldPage = e.RemovedItems.Count == 1 ? (WizardScreen) e.RemovedItems[0] : null;

            if (page != null && oldPage != page)
            {
                // Raise event
                if (oldPage != null)
                    oldPage.OnPageClose();

                // Set boundary values for navigation buttons
                if (SelectedIndex == 0)
                {
                    page.CanNavigatePrevious = false;
                    if (Items.Count == 1)
                        page.CanNavigateNext = false;
                }
                else if (SelectedIndex == Items.Count - 1)
                    page.CanNavigateNext = false;

                // After page is up and runnig, rais event
                page.OnPageShow();
            }
        }

        #endregion

        #region Protected Helpers

        protected virtual void OnFinishedClicked(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RaiseEvent(new RoutedEventArgs(FinishClickEvent, this));
        }

        protected virtual void OnNextClicked(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RaiseEvent(new RoutedEventArgs(NextClickEvent, this));

            int stepSize = ((WizardScreen) SelectedItem).NextStepSize;
            SelectedIndex += stepSize;
        }

        protected virtual void OnPreviousClicked(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RaiseEvent(new RoutedEventArgs(PreviousClickEvent, this));

            int stepSize = ((WizardScreen) SelectedItem).PreviousStepSize;
            SelectedIndex -= stepSize;
        }

        protected virtual void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RaiseEvent(new RoutedEventArgs(CancelClickEvent, this));
        }

        protected virtual void OnHelpClicked(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RaiseEvent(new RoutedEventArgs(HelpClickEvent, this));
        }

        #endregion
    }
}