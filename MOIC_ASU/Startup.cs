using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MOIC_ASU.Startup))]
namespace MOIC_ASU
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
