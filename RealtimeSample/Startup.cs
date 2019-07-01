using Microsoft.Owin;
using Owin;

namespace RealtimeSampleTask
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}