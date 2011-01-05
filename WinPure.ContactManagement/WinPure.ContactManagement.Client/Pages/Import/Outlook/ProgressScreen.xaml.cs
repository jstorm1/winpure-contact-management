using System;
using System.Collections.ObjectModel;
using System.Windows;
using WinPure.ContactManagement.Client.CommonControls;
using WinPure.ContactManagement.Client.Data.Managers.Import;

namespace WinPure.ContactManagement.Client.Pages.Import.Outlook
{
    /// <summary>
    /// Interaction logic for OutlookProgressScreen.xaml
    /// </summary>
    public partial class OutlookProgressScreen 
    {
        //// Using a DependencyProperty as the backing store for Contacts.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ContactsProperty =
        //    DependencyProperty.Register("Contacts", typeof (ObservableCollection<object>),
        //                                typeof (OutlookProgressScreen), new UIPropertyMetadata(null));


        public OutlookProgressScreen()
        {
            InitializeComponent();
        }

        //public OutlookProgressScreen(WizardControl owner):base(owner)
        //{
        //    InitializeComponent();

        //    if (owner == null) return;

        //    Owner.FinishButtonVisibility = Visibility.Hidden;
        //    Owner.Text = "Import Progress";

        //    OutlookImportManager.Current.ImportProgressCompleted += delegate
        //                                                                {

        //                                                                    Owner.FinishButtonVisibility =
        //                                                                        Visibility.Visible;
        //                                                                };

        //   // Owner.PreviousButtonClick += onOwnerOnPreviousButtonClick;
        //}

        //public ObservableCollection<object> Contacts
        //{
        //    get { return (ObservableCollection<object>) GetValue(ContactsProperty); }
        //    set { SetValue(ContactsProperty, value); }
        //}

        //private void onOwnerOnPreviousButtonClick(object sender, EventArgs e)
        //{
        //    Owner.ShowNext(new PreviewScreen(Owner));
        //    Owner.NextButtonVisibility = Visibility.Visible;
        //    Owner.PreviousButtonVisibility = Visibility.Hidden;
        //}
    }
}