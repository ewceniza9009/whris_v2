using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(whris_v2.Startup))]
namespace whris_v2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
