using System;
using System.Windows;
using WinPure.ContactManagement.Client.CommonControls;

namespace WinPure.ContactManagement.Client.Pages.Import.Outlook
{
    /// <summary>
    /// Interaction logic for PreviewScreen.xaml
    /// </summary>
    public partial class PreviewScreen : WizardScreen
    {

        public PreviewScreen(WizardControl owner = null) : base(owner)
        {
            InitializeComponent();

            if (owner == null) return;

            Owner.NextButtonClick += onOwnerOnNextButtonClick;
            Owner.NextButtonVisibility = Visibility.Visible;
        }

        private void onOwnerOnNextButtonClick(object sender, EventArgs e)
        {
            Owner.ShowNext(new OutlookProgressScreen(Owner));

            Owner.NextButtonVisibility = Visibility.Hidden;
            Owner.PreviousButtonVisibility = Visibility.Visible;
        }
    }
}