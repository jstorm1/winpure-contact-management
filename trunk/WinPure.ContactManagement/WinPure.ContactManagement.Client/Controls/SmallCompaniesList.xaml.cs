#region References

using System.Windows;
using WinPure.ContactManagement.Client.Data.Model; 

#endregion

namespace WinPure.ContactManagement.Client.Controls
{
    /// <summary>
    /// Interaction logic for SmallCompaniesList.xaml
    /// </summary>
    public partial class SmallCompaniesList
    {
        // Using a DependencyProperty as the backing store for Company.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentCompanyProperty =
            DependencyProperty.Register("CurrentCompany", typeof (Company), typeof (SmallCompaniesList),
                                        new UIPropertyMetadata(null));

        public SmallCompaniesList()
        {
            InitializeComponent();


            Loaded += onSmallCompaniesListLoaded;
        }

        void onSmallCompaniesListLoaded(object sender, RoutedEventArgs e)
        {

        }

        public Company CurrentCompany
        {
            get { return (Company)GetValue(CurrentCompanyProperty); }
            set { SetValue(CurrentCompanyProperty, value); }
        }
    }
}