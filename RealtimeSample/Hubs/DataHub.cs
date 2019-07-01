using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using RealtimeSample.Model;
using RealtimeSample.Data.Infrastructure;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RealtimeSampleTask.Hubs
{
    public class DataHub : Hub, IDisposable
    {
        private static NewMessageNotifier<DevTest> message = null;
        public static void Send(string message, SqlNotificationInfo info)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<DataHub>();
            context.Clients.All.newMessage(message, info.ToString());
        }

        private static void NotificationOnChanged(object sender, SqlNotificationEventArgs e)
        {
            Send("Changed",e.Info);
        }

        public static void RunDevNotify(IDbFactory _dbFactory, IQueryable query)
        {
            if (message == null)
            {
                var db = _dbFactory.Init();
                NewMessageNotifier<DevTest>.StartMonitor(db);
                message = new NewMessageNotifier<DevTest>(db, query);
                message.OnChanged += NotificationOnChanged;
            }
        }
        public static void EndNotify()
        {
            if (message != null)
                message.Dispose();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
          //  EndNotify();
            return base.OnDisconnected(stopCalled);
        }
    }

}