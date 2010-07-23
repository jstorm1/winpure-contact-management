#region References

using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using Transitionals.Controls; 

#endregion

namespace WinPure.ContactManagement.Client.CommonControls
{
    [TemplatePart(Name = "PART_TransitionElement", Type = typeof(TransitionElement))]
    public class PagesSliderControl : Control
    {
        #region Fields
        
        private RelayCommand<PageControl> _navigateCommand;
        private TransitionElement _transitionElement; 

        #endregion

        #region Constructor

        static PagesSliderControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (PagesSliderControl),
                                                     new FrameworkPropertyMetadata(typeof (PagesSliderControl)));
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

            _transitionElement = (TransitionElement)GetTemplateChild("PART_TransitionElement");
        }
        #endregion
    }
}