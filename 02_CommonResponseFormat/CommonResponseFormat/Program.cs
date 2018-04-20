using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;

namespace WebAPISamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string _serviceAddress = "http://localhost:3000";
            var config = new HttpSelfHostConfiguration(_serviceAddress);
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var server = new HttpSelfHostServer(config);

            server.OpenAsync().Wait();
            Console.WriteLine("Service started, address: " + _serviceAddress + "/api");
            Console.WriteLine("Press enter to stop...");
            Console.ReadLine();

            server.CloseAsync();
        }
    }
}
