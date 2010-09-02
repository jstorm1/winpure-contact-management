#region References

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
        public ShellWindow()
        {
            InitializeComponent();

            pagesSliderControl.Transition = TransitionsManager.Current.CurrentTransition;

            TransitionsManager.Current.CurrentTransitionChanged += delegate
                                                                       {
                                                                           pagesSliderControl.Transition =
                                                                               TransitionsManager.Current.
                                                                                   CurrentTransition;
                                                                       };
        }
    }
}