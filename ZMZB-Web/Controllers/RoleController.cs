using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMZB_Framework.Authorize;

namespace ZMZB_Web.Controllers
{
    [MyModule(ModuleEnum.Role, ModuleGroupEnum.BaseInfo)]
    public class RoleController : BaseController
    {
        [MyAuthorize(AuthorizeType.List, "列表")]
        public ActionResult Index()
        {
            return View();
        }
        [MyAuthorize(AuthorizeType.Add, "列表")]
        public ActionResult Add()
        {
            return View();
        }
        [MyAuthorize(AuthorizeType.Editor, "列表")]
        public ActionResult Editor()
        {
            return View();
        }

    }
}