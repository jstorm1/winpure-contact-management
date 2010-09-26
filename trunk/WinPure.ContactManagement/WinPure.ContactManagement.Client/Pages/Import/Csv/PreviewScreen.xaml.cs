using System;
using System.Windows;
using System.Windows.Controls;
using WinPure.ContactManagement.Client.CommonControls;

namespace WinPure.ContactManagement.Client.Pages.Import.Csv
{
    /// <summary>
    /// Interaction logic for PreviewScreen.xaml
    /// </summary>
    public partial class PreviewScreen 
    {
        public PreviewScreen():this(null)
        {
            
        }

        public PreviewScreen(WizardControl owner):base(owner)
        {
            InitializeComponent();

            owner.NextButtonVisibility = Visibility.Visible;
            owner.PreviousButtonVisibility = Visibility.Visible;

            owner.Text = "Preview";

            Owner.PreviousButtonClick += onOwnerOnPreviousButtonClick;
            Owner.NextButtonClick += onOwnerOnNextButtonClick;
        }

        private void onOwnerOnNextButtonClick(object sender, EventArgs e)
        {
            Owner.ShowNext(new MappingScreen());
        }

        private void onOwnerOnPreviousButtonClick(object sender, EventArgs e)
        {
            Owner.ShowNext(new SelectFileScreen(Owner));
        }
    }
}