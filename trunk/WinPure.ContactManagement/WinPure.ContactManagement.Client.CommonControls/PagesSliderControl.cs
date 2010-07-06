using System;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using Transitionals.Controls;

namespace WinPure.ContactManagement.Client.CommonControls
{
    [TemplatePart(Name = "PART_FirstPageContent", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "PART_SecondPageContent", Type = typeof(ContentPresenter))]
    public class PagesSliderControl : Control
    {
        private ContentPresenter _firstPageContentPresenter;
        private ContentPresenter _secondPageContentPresenter;
        private bool _isFirstPageVisible;
        private Button _testButton;
        private RelayCommand<PageControl> _navigateCommand;
        private TransitionElement _transitionElement;

        public RelayCommand<PageControl> NavigateCommand
        {
            get
            {
                if (_navigateCommand == null)
                    _navigateCommand = new RelayCommand<PageControl>(Navigate);
                return _navigateCommand;
            }
        }

        public void Navigate(PageControl page)
        {
            _transitionElement.Content = page;

            //if (_isFirstPageVisible)
            //{
            //    if (_firstPageContentPresenter.Content == page) return;
            //    _secondPageContentPresenter.Content = page;
            //    VisualStateManager.GoToState(this, "ShowSecondPage", true);
            //    _isFirstPageVisible = false;
            //}
            //else
            //{
            //    if (_secondPageContentPresenter.Content == page) return;
            //    _firstPageContentPresenter.Content = page;
            //    VisualStateManager.GoToState(this, "ShowFirstPage", true);
            //    _isFirstPageVisible = true;
            //}
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _firstPageContentPresenter = (ContentPresenter)GetTemplateChild("PART_FirstPageContent");
            _secondPageContentPresenter = (ContentPresenter)GetTemplateChild("PART_SecondPageContent");

            _transitionElement = (TransitionElement)GetTemplateChild("transitionElement");

            _testButton = (Button)GetTemplateChild("TestButton");
            _testButton.Click += _testButton_Click;
        }

        private void _testButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isFirstPageVisible)
            {
                VisualStateManager.GoToState(this, "ShowSecondPage", true);
                _isFirstPageVisible = false;
            }
            else
            {
                VisualStateManager.GoToState(this, "ShowFirstPage", true);
                _isFirstPageVisible = true;
            }
        }

        #region Constructor

        static PagesSliderControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PagesSliderControl),
                                                     new FrameworkPropertyMetadata(typeof(PagesSliderControl)));
        }

        #endregion
    }
}