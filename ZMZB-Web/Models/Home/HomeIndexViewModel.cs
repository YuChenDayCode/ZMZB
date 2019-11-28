using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZMZB_Entity.Entity;
using ZMZB_Framework.Menu;

namespace ZMZB_Web.Models.Home
{
    public class HomeIndexViewModel
    {
        public string LoginId { get; set; }

        public bool IsDebug { get; set; }

        public IEnumerable<AuthorizeEntity> Authoritys { get; set; }

        public IEnumerable<MenuItem> MenuItmes { get; set; }
    }
}