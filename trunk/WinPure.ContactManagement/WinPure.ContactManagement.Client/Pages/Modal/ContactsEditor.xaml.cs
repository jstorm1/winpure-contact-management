#region References

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WinPure.ContactManagement.Client.CommonControls;
using WinPure.ContactManagement.Client.Controls;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Common.Helpers;
using WinPure.ContactManagement.Client.ViewModels;

#endregion

namespace WinPure.ContactManagement.Client.Pages.Modal
{
    /// <summary>
    /// Interaction logic for ContactsEditor.xaml
    /// </summary>
    public partial class ContactsEditor : ModalDialog
    {
        // Using a DependencyProperty as the backing store for CurrentContact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentContactProperty =
            DependencyProperty.Register("CurrentContact", typeof (Contact), typeof (ContactsEditor),
                                        new UIPropertyMetadata(onCurrentContactChanged));

        public ContactsEditor(Contact contact = null)
        {
            InitializeComponent();

            if (contact == null)
                contact = new Contact {ContactID = Guid.NewGuid(), FirstName = "", LastName = ""};

            CurrentContact = contact;
        }

        private ContactEditorViewModel viewModel
        {
            get { return (ContactEditorViewModel) DataContext; }
        }

        public Contact CurrentContact
        {
            get { return (Contact) GetValue(CurrentContactProperty); }
            set { SetValue(CurrentContactProperty, value); }
        }

        private void onSaveButtonClick(object sender, RoutedEventArgs e)
        {
            updateBindingsOnGeneralTab();
            updateBindingOnCompanyTab();
            updateBindingOnContactsTab();

            Close();
        }

        private void updateBindingOnContactsTab()
        {
            ContactsGrid.UpdateBindingSources<TextBox>(TextBox.TextProperty);
        }

        private void updateBindingOnCompanyTab()
        {
            CompanyGrid.UpdateBindingSources<TextBox>(TextBox.TextProperty);
            CompanyGrid.UpdateBindingSources<SmallCompaniesList>(SmallCompaniesList.CurrentCompanyProperty);
        }

        private void updateBindingsOnGeneralTab()
        {
            GeneralGrid.UpdateBindingSources<TextBox>(TextBox.TextProperty);
            GeneralGrid.UpdateBindingSources<ComboBox>(Selector.SelectedItemProperty);
        }

        private void onCancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private static void onCurrentContactChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ContactsEditor;
            if (control == null) return;

            var contact = e.NewValue as Contact;
            if (contact == null) return;

            control.viewModel.Contact = contact;
        }
    }
}