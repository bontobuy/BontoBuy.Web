using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BontoBuy.Web.Startup))]
namespace BontoBuy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
