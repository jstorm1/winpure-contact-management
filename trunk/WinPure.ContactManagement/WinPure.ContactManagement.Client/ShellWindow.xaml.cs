#region References

using System.ComponentModel;
using System.Windows;
using WinPure.ContactManagement.Client.Data.Managers;

#endregion

namespace WinPure.ContactManagement.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ShellWindow()
        {
            InitializeComponent();

            //Check for Design mode.
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            PagesSliderControl.Transition = TransitionsManager.Current.CurrentTransition;

            TransitionsManager.Current.CurrentTransitionChanged += delegate
                                                                       {
                                                                           PagesSliderControl.Transition =
                                                                               TransitionsManager.Current.
                                                                                   CurrentTransition;
                                                                       };
        }
    }
}