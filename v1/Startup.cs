using Owin;
using Microsoft.Extensions.Hosting;

namespace API
{
    public partial class Startup
    {
        

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<QueueStorageService>();
        }
    }
}