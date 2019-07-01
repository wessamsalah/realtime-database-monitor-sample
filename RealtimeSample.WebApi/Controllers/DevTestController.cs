using RealtimeSample.Data.Infrastructure;
using RealtimeSample.Model;
using RealtimeSample.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RealtimeSample.WebApi.Controllers
{
    public class DevTestController : ApiController
    {
        #region Properties


        private readonly IDevTestService _devTestService;
        private readonly IDbFactory _dbFactory;

        #endregion

        #region Ctro

        public DevTestController(IDevTestService DevTestService, IDbFactory dbFactory)
        {
            _devTestService = DevTestService;
            _dbFactory = dbFactory;
        }

        #endregion

        #region APIs

        public IEnumerable<DevTest> Get()
        {
            return _devTestService.GetAll();
        }

        public IHttpActionResult GetById(int id)
        {
            var devTest = _devTestService.GetById(id);
            if (devTest != null)
                return Ok(devTest);
            else
                return NotFound();
        }

        public IHttpActionResult GetLastUpdated(DateTime lastupdate)
        {
            try
            {
                IEnumerable<DevTest> lst = new List<DevTest>();
                if (lastupdate != null)
                {
                    lst = _devTestService.GetAllAfterTime(d => d.Date > lastupdate);
                    return Ok(lst);
                }
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


        //[Route("api/DevTest/deleted")]
        //public IHttpActionResult GetDeleted(DateTime lastupdate)
        //{
        //    var lst = _devTestService.GetAllAfterTime(d => d.Date > lastupdate & d.Isdeleted == true).Select(u=>u.ID);
        //    return Ok(lst);
        //}

        public IHttpActionResult Post(DevTest value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    value.Date = DateTime.Now;
                    _devTestService.Add(value);
                    _devTestService.Save();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        public IHttpActionResult Put(int id, DevTest value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    value.Date = DateTime.Now;
                    _devTestService.Update(value);
                    _devTestService.Save();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var devTest = _devTestService.GetById(id);
                _devTestService.Delete(devTest);
                _devTestService.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            //devTest.Isdeleted = true;
            //devTest.Date = DateTime.Now;
            //_devTestService.Update(devTest);

        }


    }

    //public static class Common
    //{
    //    public static IHttpActionResult GlobalException(Action Method)
    //    {
    //        try
    //        {
    //            Method();
    //        }
    //        catch (Exception)
    //        {
    //            return b
    //        }

    //    }
    //}
    #endregion
}
