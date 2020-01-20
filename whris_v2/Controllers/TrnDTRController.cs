using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.DynamicLinq;

namespace whris_v2.Controllers
{
    public class TrnDTRController : Controller
    {
        public Data.whrisDataContext whris;

        #region View
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        public JsonResult GetDTRList(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter) 
        {
            if (filter != null)
            {
                if (filter.Filters != null)
                {
                    foreach (var f in filter.Filters)
                    {
                        if (f.Field == "PayrollGroupId" && f.Value != null)
                        {
                            f.Value = int.Parse(f.Value.ToString());
                        }
                        else if(f.Field == "PayrollGroupId" && f.Value == null) 
                        {
                            filter = null;
                        }
                    }
                }
                
            }

            using (whris = new Data.whrisDataContext()) 
            {
                var result = whris.TrnDTRs
                    .Select(x => new Models.TrnDTR
                    {
                        Id = x.Id,
                        DTRDate = x.DTRDate,
                        DTRNumber = x.DTRNumber,
                        DateStart = x.DateStart,
                        DateEnd = x.DateEnd,
                        Remarks = x.Remarks,
                        PayrollGroupId = x.PayrollGroupId,
                        IsLocked = x.IsLocked
                    }).ToDataSourceResult(take, skip, sort, filter);

                return Json(result);
            }
        }

        public JsonResult CmbFilterByPayrollGroup()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstPayrollGroups
                         select new Models.ComboBox.MstEmployee.CmbPayrollGroup
                         { 
                            Id = i.Id,
                            PayrollGroup = i.PayrollGroup
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}