using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TPA_DatingMVC.Startup))]
namespace TPA_DatingMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
