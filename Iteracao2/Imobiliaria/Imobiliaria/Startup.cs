using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Imobiliaria.Startup))]
namespace Imobiliaria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
