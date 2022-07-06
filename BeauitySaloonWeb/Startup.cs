using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeauitySaloonWeb.Startup))]
namespace BeauitySaloonWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
