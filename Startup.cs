using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SellMyStuff.Startup))]
namespace SellMyStuff
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
