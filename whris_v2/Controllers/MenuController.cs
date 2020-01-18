using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace whris_v2.Controllers
{
    public class MenuController : Controller
    {
        public ActionResult Index(string search = "")
        {
            return PartialView(new Services.Menu(search).AllActiveMenus);
        }

        [HttpPost]
        public ActionResult Search(string search = "") 
        {
            return PartialView("Index", new Services.Menu(search).AllActiveMenus);
        }
    }
}