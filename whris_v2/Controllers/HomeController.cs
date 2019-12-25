using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using whris_v2.Data;

namespace whris_v2.Controllers
{
    public class HomeController : Controller
    {
        public whrisDataContext whris;
        public JsonResult GetUserList()
        {
            whris = new whrisDataContext();

            var userList = whris.MstUsers
                .Select(i => new Models.MstUser
                {
                    Id = i.Id,
                    Username = i.UserName,
                    FullName = i.FullName,
                    IsLocked = i.IsLocked
                });

            return Json(userList.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}