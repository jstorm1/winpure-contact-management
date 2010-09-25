using WinPure.ContactManagement.Client.CommonControls;

namespace WinPure.ContactManagement.Client.Pages.Import.Outlook
{
    /// <summary>
    /// Interaction logic for PreviewScreen.xaml
    /// </summary>
    public partial class PreviewScreen : WizardScreen
    {
        public PreviewScreen() : this(null)
        {
        }

        public PreviewScreen(WizardControl owner) : base(owner)
        {
            InitializeComponent();
        }
    }
}