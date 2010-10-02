using System;
using System.Windows;
using System.Windows.Controls;
using WinPure.ContactManagement.Client.ViewModels.Settings;

namespace WinPure.ContactManagement.Client.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            SettingsTabControl.SelectionChanged += (s,e) =>
                                                       {
                                                           if (e.AddedItems.Count > 0 && e.AddedItems[0] == ViewTabItem)
                                                           {
                                                               ((ViewTabViewModel)ViewTabItem.DataContext).RefreshTransitionsList();
                                                           }
                                                       };
        }
    }
}