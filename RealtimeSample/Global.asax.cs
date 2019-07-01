using RealtimeSample.Data.Infrastructure;
using RealtimeSample.Data.Repositories;
using RealtimeSample.Service;
using RealtimeSampleTask.App_Start;
using RealtimeSampleTask.Hubs;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace RealtimeSampleTask
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.GetConfiguredContainer();
            //var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ClickTrackerEntities"].ConnectionString;
            //SqlDependency.Start(connectionString);
        }
        protected void Application_End()
        {
        }
    }
}
