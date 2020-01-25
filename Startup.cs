using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymBusiness.Startup))]
namespace GymBusiness
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
