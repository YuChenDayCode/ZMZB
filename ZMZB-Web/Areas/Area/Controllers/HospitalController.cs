using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZMZB_Web.Areas.Area.Controllers
{
    public class HospitalController : Controller
    {
        // GET: Area/Hospital
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult queryReturnMoneyApplyHos()
        {
            return View();
        }
    }
}