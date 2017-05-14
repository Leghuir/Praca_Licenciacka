using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MojDziennikv4.Startup))]
namespace MojDziennikv4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
