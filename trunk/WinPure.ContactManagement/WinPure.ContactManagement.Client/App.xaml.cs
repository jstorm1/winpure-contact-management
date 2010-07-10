using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Windows;
using WinPure.ContactManagement.Client.Services;

namespace WinPure.ContactManagement.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SyncServiceManager.Current.RunService();
            SyncServiceManager.Current.GetAdderessesOfService();


            //DiscoveryClient discoverclient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            //FindResponse response = discoverclient.Find(new FindCriteria(typeof(ISyncService)));

            //var str = "";
            //foreach (var endpoint in response.Endpoints)
            //{
            //    str += endpoint.Address + "\n";
            //}

            //MessageBox.Show(str);

            //EndpointAddress address = response.Endpoints[0].Address;
            
            
            //SyncServiceClient client = new SyncServiceClient(new WSHttpBinding(), address);
            //string str= client.GetMessage("Hello WCF 4 ");
           // MessageBox.Show(str);

        }
    }
}
