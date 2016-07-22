using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;

namespace RpiLaserHost
{
    public class Program
    {
        static void Main(string[] args)
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
                Console.WriteLine($"Web Server is running on: {baseAddress}");
                Console.WriteLine("Press any key to quit.");
                Console.ReadLine();
            }
        }
    }
}
