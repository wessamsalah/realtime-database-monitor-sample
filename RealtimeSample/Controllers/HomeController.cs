using RealtimeSample.Data.Infrastructure;
using RealtimeSample.Model;
using RealtimeSample.Service;
using RealtimeSampleTask.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealtimeSampleTask.Controllers
{
    public class HomeController : Controller, IDisposable
    {
        #region Properties
        private readonly IDevTestService _devTestService;
        private readonly IDbFactory _dbFactory;
        #endregion

        #region Ctro
        public HomeController(IDevTestService DevTestService, IDbFactory dbFactory)
        {
            _devTestService = DevTestService;
            _dbFactory = dbFactory;
        }
        #endregion

        #region Controllers

        public ActionResult Index()
        {
            var query = _dbFactory.Init().DevTests.AsQueryable();
            Hubs.DataHub.RunDevNotify(_dbFactory, query);

            return View();
        }


        public ActionResult Add()
        {
            ViewBag.Message = "Add";

            return View();
        }

        public ActionResult Update()
        {
            ViewBag.Message = "Update";

            return View();
        }
        #endregion

    }
}