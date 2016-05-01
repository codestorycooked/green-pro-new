using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreenPro.WebClient.Startup))]
namespace GreenPro.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
