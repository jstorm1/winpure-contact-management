using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace WinPure.ContactManagement.Client.CommonControls
{
    [TemplatePart(Name = "PART_NextButton", Type = typeof (Button))]
    [TemplatePart(Name = "PART_PreviousButton", Type = typeof (Button))]
    [TemplatePart(Name = "PART_FinishButton", Type = typeof (Button))]
    public class WizardControl : ModalDialog
    {
        #region Events

        public event EventHandler NextButtonClick;
        public event EventHandler PreviousButtonClick;

        #endregion

        // Using a DependencyProperty as the backing store for NextButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextButtonVisibilityProperty =
            DependencyProperty.Register("NextButtonVisibility", typeof (Visibility), typeof (WizardControl),
                                        new UIPropertyMetadata(Visibility.Hidden));


        // Using a DependencyProperty as the backing store for PreviousButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviousButtonVisibilityProperty =
            DependencyProperty.Register("PreviousButtonVisibility", typeof (Visibility), typeof (WizardControl),
                                        new UIPropertyMetadata(Visibility.Hidden));


        // Using a DependencyProperty as the backing store for Sequence.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SequenceProperty =
            DependencyProperty.Register("Sequence", typeof (Queue<WizardScreen>), typeof (WizardControl),
                                        new UIPropertyMetadata(null));


        // Using a DependencyProperty as the backing store for History.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HistoryProperty =
            DependencyProperty.Register("History", typeof (Stack<WizardScreen>), typeof (ModalDialog),
                                        new UIPropertyMetadata(null));

        private Button _finishButton;

        private Button _nextButton;
        private Button _previousButton;


        static WizardControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (WizardControl),
                                                     new FrameworkPropertyMetadata(typeof (WizardControl)));
        }

        public WizardControl()
        {
            Sequence = new Queue<WizardScreen>();
            History = new Stack<WizardScreen>();
        }


        [Category("Layout")]
        public Visibility NextButtonVisibility
        {
            get { return (Visibility) GetValue(NextButtonVisibilityProperty); }
            set { SetValue(NextButtonVisibilityProperty, value); }
        }

        [Category("Layout")]
        public Visibility PreviousButtonVisibility
        {
            get { return (Visibility) GetValue(PreviousButtonVisibilityProperty); }
            set { SetValue(PreviousButtonVisibilityProperty, value); }
        }


        public Stack<WizardScreen> History
        {
            get { return (Stack<WizardScreen>)GetValue(HistoryProperty); }
            private set { SetValue(HistoryProperty, value); }
        }

        public Queue<WizardScreen> Sequence
        {
            get { return (Queue<WizardScreen>) GetValue(SequenceProperty); }
            private set { SetValue(SequenceProperty, value); }
        }


        public override void OnApplyTemplate()
        {
            _nextButton = GetTemplateChild("PART_NextButton") as Button;
            if (_nextButton == null)
                return;

            _previousButton = GetTemplateChild("PART_PreviousButton") as Button;
            if (_previousButton == null)
                return;

            _finishButton = GetTemplateChild("PART_FinishButton") as Button;
            if (_finishButton == null)
                return;

            base.OnApplyTemplate();

            _nextButton.Click += onNextButtonOnClick;

            _previousButton.Click += onPreviousButtonOnClick;

            _finishButton.Click += onFinishButtonOnClick;
        }

        private void onFinishButtonOnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void onNextButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (NextButtonClick != null)
                NextButtonClick.Invoke(this, EventArgs.Empty);
        }

        private void onPreviousButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (PreviousButtonClick != null)
                PreviousButtonClick.Invoke(this, EventArgs.Empty);
        }

        public WizardScreen GetNextScreen()
        {
            WizardScreen screen = Sequence.Dequeue();
            screen.Owner = this;
            return screen;
        }

        public void ShowNext(WizardScreen screen)
        {
            Content = screen;
            Debug.WriteLine(screen.GetType().Name.ToString());
            if (!History.Contains(screen))
                History.Push(screen);
        }

        public void ShowPrevious()
        {
            //History.Pop();
            WizardScreen screen = History.Pop();
            Content = screen;
            Debug.WriteLine(screen.GetType().Name.ToString());
            if (!Sequence.Contains(screen))
                Sequence.Enqueue(screen);
        }
    }
}