using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(libapp.Startup))]
namespace libapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
