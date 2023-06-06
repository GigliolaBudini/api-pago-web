using API.Endpoints.PagoCuentas.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace API
{
    /// <summary>
    /// Web Api Bootstrap
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Start up
        /// </summary>
       
        private IHost _host;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            _host = CreateHostBuilder().Build();
            _host.Start();
        }

        protected void Application_Stop()
        {
            _host?.Dispose();
        }

        private IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureLogging(logging => 
                {
                    logging.ClearProviders(); 
                    
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<QueueService>();
                });
        }


    }
}
