using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WordPress.Startup))]
namespace WordPress
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
