using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Academy.Web.Startup))]
namespace Academy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
