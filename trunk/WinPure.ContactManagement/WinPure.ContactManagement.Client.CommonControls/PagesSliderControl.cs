#region References

using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using Transitionals;
using Transitionals.Controls;

#endregion

namespace WinPure.ContactManagement.Client.CommonControls
{
    [TemplatePart(Name = "PART_TransitionElement", Type = typeof (TransitionElement))]
    public class PagesSliderControl : Control
    {
        #region Fields

        private RelayCommand<PageControl> _navigateCommand;
        private TransitionElement _transitionElement;

        #endregion

        #region Dependency property

        // Using a DependencyProperty as the backing store for Transition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TransitionProperty =
            DependencyProperty.Register("Transition", typeof (Transition), typeof (PagesSliderControl),
                                        new UIPropertyMetadata(null));

        #endregion

        #region Constructor

        static PagesSliderControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (PagesSliderControl),
                                                     new FrameworkPropertyMetadata(typeof (PagesSliderControl)));
        }

        #endregion

        #region Properties

        public Transition Transition
        {
            get { return (Transition) GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        #endregion

        #region Commands

        public RelayCommand<PageControl> NavigateCommand
        {
            get { return _navigateCommand ?? (_navigateCommand = new RelayCommand<PageControl>(navigate)); }
        }

        #endregion

        #region Methods

        private void navigate(PageControl page)
        {
            _transitionElement.Content = page;
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _transitionElement = (TransitionElement) GetTemplateChild("PART_TransitionElement");
        }

        #endregion
    }
}