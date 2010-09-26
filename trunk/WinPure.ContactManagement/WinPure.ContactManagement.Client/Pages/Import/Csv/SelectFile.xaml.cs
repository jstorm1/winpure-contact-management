using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinPure.ContactManagement.Client.CommonControls;

namespace WinPure.ContactManagement.Client.Pages.Import.Csv
{
    /// <summary>
    /// Interaction logic for SelectFile.xaml
    /// </summary>
    public partial class SelectFile
    {
        public SelectFile():this(null)
        {
            
        }

        public SelectFile(WizardControl owner):base(owner)
        {
            InitializeComponent();

            initialize();
        }

        private void initialize()
        {
            Owner.Text = "Select File";
            Owner.NextButtonVisibility = Visibility.Visible;

            Owner.NextButtonClick += onOwnerNextButtonClick;
        }

        void onOwnerNextButtonClick(object sender, EventArgs e)
        {
            
        }
    }
}
