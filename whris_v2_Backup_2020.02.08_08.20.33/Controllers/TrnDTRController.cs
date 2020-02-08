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

        public ActionResult TrnDTRDetail(int modelId)
        {
            var result = new Models.TrnDTR();
            var data = new Data.TrnDTR();

            var mappingProfile = new Mapping.MappingProfile<Data.TrnDTR, Models.TrnDTR>();

            using (whris = new Data.whrisDataContext())
            {
                data = whris.TrnDTRs.Where(x => x.Id == modelId).FirstOrDefault();

                if (modelId == 0)
                {
                    var DefaultPeriodId = whris.MstPeriods.LastOrDefault().Id;

                    var maxDTRNumber = Int64.Parse(whris.TrnDTRs.Max(x => x.DTRNumber).Substring(5, 6));

                    var DefaultDTRNumber = DateTime.Now.Year + "-" + (maxDTRNumber + 1000001).ToString().Substring(1, 6);

                    var DefaultPayrollGroupId = whris.MstPayrollGroups.FirstOrDefault().Id;
                    var DefaultDTRDate = DateTime.Now;


                    data = new Data.TrnDTR
                    {
                        Id = 0,
                        PeriodId = DefaultPeriodId,
                        DTRNumber = DefaultDTRNumber,
                        DTRDate = DefaultDTRDate,
                        PayrollGroupId = DefaultPayrollGroupId,
                        DateStart = DefaultDTRDate,
                        DateEnd = DefaultDTRDate,
                        Remarks = "NA",
                        EntryUserId = 1,
                        EntryDateTime = DateTime.Now,
                        UpdateUserId = 1,
                        UpdateDateTime = DateTime.Now,
                        IsLocked = false
                    };
                }
            }

            result = mappingProfile.mapper.Map<Data.TrnDTR, Models.TrnDTR>(data);

            return View(result);
        }

        public JsonResult ReadDTRLines(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter, int modelFilterId) 
        {
            using (whris = new Data.whrisDataContext()) 
            {
                var result = whris.TrnDTRLines
                    .Where(x => x.DTRId == modelFilterId)
                    .Select(x => new Models.TrnDTRLine()
                    {
                    });
            }

            return null;
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
                        else if (f.Field == "PayrollGroupId" && f.Value == null)
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

        public JsonResult CmbPeriod()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstPeriods
                         select new Models.ComboBox.MstEmployee.CmbPeriod
                         {
                             Id = i.Id,
                             Period = i.Period
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CmbPayrollGroup() 
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

        public JsonResult CmbOvertime() 
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.TrnOverTimes
                         select new Models.ComboBox.TrnDTR.CmbOvertime
                         {
                             Id = i.Id,
                             OTNumber = i.OTNumber
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CmbLeaveApplication() 
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.TrnLeaveApplications
                         select new Models.ComboBox.TrnDTR.CmbLeaveApplication
                         {
                             Id = i.Id,
                             LANumber = i.LANumber
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CmbChangeShift() 
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.TrnChangeShifts
                         select new Models.ComboBox.TrnDTR.CmbChangeShift
                         {
                             Id = i.Id,
                             CSNumber = i.CSNumber
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CmbPreparedBy() 
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstUsers
                         select new Models.ComboBox.MstEmployee.CmbUser
                         {
                             Id = i.Id,
                             UserName = i.UserName
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CmbCheckedBy()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstUsers
                         select new Models.ComboBox.MstEmployee.CmbUser
                         {
                             Id = i.Id,
                             UserName = i.UserName
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CmbApprovedBy()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstUsers
                         select new Models.ComboBox.MstEmployee.CmbUser
                         {
                             Id = i.Id,
                             UserName = i.UserName
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
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