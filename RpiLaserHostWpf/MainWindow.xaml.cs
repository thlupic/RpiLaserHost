using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Owin.Hosting;

namespace RpiLaserHostWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // provjeriti dal ovo radi
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowTextViewModel();
            
            this.Loaded += (s,e) => OnLoaded();
        }
        
        private void OnLoaded()
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

            // cijeli using u Task.Run
            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                var console = MainWindow.DataContext as String; 
                
                // ovdje treba staviti while(true) {thread.sleep(100);}
                console+= ($"Web Server is running on: {baseAddress}");
            }
        }
        }
    }
}
