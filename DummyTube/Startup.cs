using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DummyTube.Startup))]
namespace DummyTube
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
