using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreenPro.AdminInterface.Startup))]
namespace GreenPro.AdminInterface
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
