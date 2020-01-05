using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace whris_v2.Controllers
{
    public class SecurityController : Controller
    {
        public JsonResult CurrentFormRights(string form) 
        {
            var formRights = new Services.Security(form);

            return Json(formRights, JsonRequestBehavior.AllowGet);
        }
    }
}