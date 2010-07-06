#region MyRegion

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WinPure.ContactManagement.Client.CommonControls;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Client.Helpers;
using WinPure.ContactManagement.Client.ViewModels;

#endregion

namespace WinPure.ContactManagement.Client.Pages.Modal
{
    /// <summary>
    /// Interaction logic for CompaniesEditor.xaml
    /// </summary>
    public partial class CompaniesEditor : ModalDialog
    {
        // Using a DependencyProperty as the backing store for Company.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentCompanyProperty =
            DependencyProperty.Register("CurrentCompany", typeof(Company), typeof(CompaniesEditor),
                                        new UIPropertyMetadata(onCurrentCompanyPropertyChanged));


        public CompaniesEditor(Company company = null)
        {
            InitializeComponent();

            if (company == null)
                company = new Company();

            CurrentCompany = company;
        }

        public Company CurrentCompany
        {
            get { return (Company)GetValue(CurrentCompanyProperty); }
            set { SetValue(CurrentCompanyProperty, value); }
        }

        private CompanyEditorViewModel viewModel
        {
            get { return DataContext as CompanyEditorViewModel; }
        }

        private static void onCurrentCompanyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CompaniesEditor;
            if (control == null) return;

            var company = e.NewValue as Company;
            if (company == null) return;

            control.viewModel.Company = company;
        }

        private void onCancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void onSaveButtonClick(object sender, RoutedEventArgs e)
        {
            var textBoxes =  this.GetChildObjects<TextBox>();
            var bindingExpressions =
                textBoxes.Select(textBox => textBox.GetBindingExpression(TextBox.TextProperty)).Where(
                    bindingExpression => bindingExpression != null);
            
            foreach (var bindingExpression in bindingExpressions)
            {
                bindingExpression.UpdateSource();
            }

            Close();
        }
    }
}