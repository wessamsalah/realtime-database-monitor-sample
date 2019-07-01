using RealtimeSample.Data.Infrastructure;
using RealtimeSample.Data.Repositories;
using RealtimeSample.Service;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;


namespace RealtimeSample.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IDbFactory, DbFactory>(new PerResolveLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerResolveLifetimeManager());
            container.RegisterType<IDevTestRepository, DevTestRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IDevTestService, DevTestService>(new PerResolveLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}