using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EstagiosDEIS.Startup))]
namespace EstagiosDEIS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
