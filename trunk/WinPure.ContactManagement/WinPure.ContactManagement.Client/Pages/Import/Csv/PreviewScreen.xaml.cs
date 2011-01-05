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
        public PreviewScreen()
        {
            InitializeComponent();
        }

        //public PreviewScreen(WizardControl owner):base(owner)
        //{
        //    InitializeComponent();

        //    Initialize();
        //}

        //public void Initialize()
        //{
        //    Owner.NextButtonVisibility = Visibility.Visible;
        //    Owner.PreviousButtonVisibility = Visibility.Visible;

        //    Owner.Text = "Preview";

        //    Owner.PreviousButtonClick += onOwnerOnPreviousButtonClick;
        //    Owner.NextButtonClick += onOwnerOnNextButtonClick;
        //}

        //private void onOwnerOnNextButtonClick(object sender, EventArgs e)
        //{
        //    Owner.ShowNext(new MappingScreen());


        //    Owner.PreviousButtonClick -= onOwnerOnPreviousButtonClick;
        //    Owner.NextButtonClick -= onOwnerOnNextButtonClick;
        //}

        //private void onOwnerOnPreviousButtonClick(object sender, EventArgs e)
        //{
        //    Owner.ShowNext(new SelectFileScreen(Owner));

        //    Owner.PreviousButtonClick -= onOwnerOnPreviousButtonClick;
        //    Owner.NextButtonClick -= onOwnerOnNextButtonClick;
        //}
    }
}