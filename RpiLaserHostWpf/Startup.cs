using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace RpiLaserHostWpf
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseCors(CorsOptions.AllowAll);

            var hubConfiguration = new HubConfiguration
            {
                // You can enable JSONP by uncommenting line below.
                // JSONP requests are insecure but some older browsers (and some
                // versions of IE) require JSONP to work cross domain
                // EnableJSONP = true
            };

            // Map signalR
            appBuilder.MapSignalR(hubConfiguration);

            appBuilder.UseWebApi(config);
        }
    }
}
