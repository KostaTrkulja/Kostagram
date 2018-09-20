using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kostagram_1._1.Startup))]
namespace Kostagram_1._1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
