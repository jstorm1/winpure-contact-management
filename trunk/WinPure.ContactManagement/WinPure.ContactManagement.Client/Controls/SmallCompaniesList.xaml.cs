using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WinPure.ContactManagement.Client.Data.Model;

namespace WinPure.ContactManagement.Client.Controls
{
    /// <summary>
    /// Interaction logic for SmallCompaniesList.xaml
    /// </summary>
    public partial class SmallCompaniesList : UserControl
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
            ////Check for Design mode.
            //if (DesignerProperties.GetIsInDesignMode(this)) return;

            //if (CurrentCompany != null)
            //{
            //    var company = ((ObservableCollection<Company>)CompaniesList.ItemsSource).Where(c => c.CompanyId == CurrentCompany.CompanyId).FirstOrDefault();
            //    CompaniesList.SelectedItem = company;
            //}
        }

        public Company CurrentCompany
        {
            get { return (Company)GetValue(CurrentCompanyProperty); }
            set { SetValue(CurrentCompanyProperty, value); }
        }
    }
}