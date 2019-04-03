using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Academy.Startup))]
namespace Academy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
