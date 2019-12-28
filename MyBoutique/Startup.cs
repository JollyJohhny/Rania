using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyBoutique.Startup))]
namespace MyBoutique
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
