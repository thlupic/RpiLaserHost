using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Owin.Hosting;

namespace RpiLaserHostWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            var ipSelected = string.Empty;

            foreach (var ip in host.AddressList)
            {
                var ipAddress = ip.ToString().Split('.');

                if (ipAddress[0].Equals("192") && ipAddress[1].Equals("168") && ipAddress[2].Equals("1"))
                {
                    ipSelected = ip.ToString();
                }
            }

            var baseAddress = $"http://localhost:9000/";

            if (!string.IsNullOrWhiteSpace(ipSelected))
            {
                baseAddress = $"http://{ipSelected}:9000/";
            }

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                MainWindow =  ($"Web Server is running on: {baseAddress}");
            }
        }
    }
}
