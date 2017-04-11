using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebPage.Startup))]
namespace WebPage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
