using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cereris.Startup))]
namespace Cereris
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
