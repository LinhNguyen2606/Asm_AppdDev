using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Asm_AppdDev.Startup))]
namespace Asm_AppdDev
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
