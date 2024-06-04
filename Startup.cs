using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LicoreriaTom.Startup))]
namespace LicoreriaTom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
