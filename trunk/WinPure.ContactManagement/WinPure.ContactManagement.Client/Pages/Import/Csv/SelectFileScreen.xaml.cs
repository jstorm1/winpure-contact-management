using System;
using System.Windows;
using WinPure.ContactManagement.Client.CommonControls;

namespace WinPure.ContactManagement.Client.Pages.Import.Csv
{
    /// <summary>
    /// Interaction logic for SelectFileScreen.xaml
    /// </summary>
    public partial class SelectFileScreen
    {
        public SelectFileScreen() : this(null)
        {
        }

        public SelectFileScreen(WizardControl owner) : base(owner)
        {
            IsReadyToContinue = false;

            InitializeComponent();

            initialize();
        }


        private void initialize()
        {
            Owner.Text = "Select File";
            Owner.PreviousButtonVisibility = Visibility.Hidden;

            Owner.NextButtonClick += onOwnerNextButtonClick;
        }

        private void onOwnerNextButtonClick(object sender, EventArgs e)
        {
            Owner.ShowNext(new PreviewScreen(Owner));
        }
    }
}