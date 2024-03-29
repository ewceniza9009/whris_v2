﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.DynamicLinq;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using Convert = System.Convert;

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

        #region DTR
        [HttpPost]
        public void ImportExcel()
        {
            const string dtrFile = "DTRFile";

            if (Request.Files != null && (Request.Files[dtrFile]?.ContentLength ?? 0) > 0)
            {
                string extension = Path.GetExtension(Request.Files[dtrFile].FileName)?.ToLower();

                var dt = new DataTable();

                var departmentId = 0;
                var employeeId = 0;
                var startDate = DateTime.Now.Date;
                var endDate = DateTime.Now.Date;

                if (!string.IsNullOrEmpty(Request.Form[0])) departmentId = int.Parse(Request.Form[0]);
                if (!string.IsNullOrEmpty(Request.Form[1])) employeeId = int.Parse(Request.Form[1]);
                if (!string.IsNullOrEmpty(Request.Form[2])) startDate = DateTime.Parse(Request.Form[2]).Date;
                if (!string.IsNullOrEmpty(Request.Form[3])) endDate = DateTime.Parse(Request.Form[3]).Date;

                string[] validFileTypes = { ".xls", ".xlsx", ".csv", ".txt" };
                string path1 = $"{Server.MapPath("~/Content/Uploads")}/{Request.Files[dtrFile].FileName}";

                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    {
                        System.IO.File.Delete(path1);
                    }
                    Request.Files[dtrFile].SaveAs(path1);
                    if (extension == ".csv" || extension == ".txt")
                    {
                        dt = Services.ExcelUploadUtil.ConvertToDataTable(path1);
                        ViewBag.Data = dt;
                    }
                    else
                    {
                        var connString = "";
                        if (extension != null && extension.Trim() == ".xls")
                        {
                            connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            dt = Services.ExcelUploadUtil.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }
                        else if (extension != null && extension.Trim() == ".xlsx")
                        {
                            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            dt = Services.ExcelUploadUtil.ConvertXSLXtoDataTable(path1, connString);
                            ViewBag.Data = dt;
                        }
                    }
                }
                else
                {
                    ViewBag.Error = "Please Upload Files in .xls, .xlsx, .csv or .txt format";
                }


                using (var ctx = new Data.whrisDataContext())
                {
                    var allDtr = ctx.TrnDTRLogs.Where(x => x.Date.Date >= startDate && x.Date <= endDate);

                    ctx.TrnDTRLogs.DeleteAllOnSubmit(allDtr);

                    foreach (DataRow row in dt.Rows)
                    {
                        var dtr = new Models.DataTables.DTR
                        {
                            No = Convert.ToInt32(row["No"]),
                            TMNo = Convert.ToInt32(row["TMNo"]),
                            EnNo = Convert.ToInt32(row["EnNo"]),
                            Name = row["Name"].ToString(),
                            INOUT = Convert.ToInt32(row["INOUT"]),
                            Mode = Convert.ToInt32(row["Mode"]),
                            DateTime = Convert.ToDateTime(row["DateTime"])
                        };
                    }
                }
            }
        }
        public ActionResult TrnDTRDetail(int modelId)
        {
            var result = new Models.TrnDTR();
            var data = new Data.TrnDTR();

            var mappingProfile = new Mapping.MappingProfile<Data.TrnDTR, Models.TrnDTR>();

            using (whris = new Data.whrisDataContext())
            {
                data = whris.TrnDTRs.FirstOrDefault(x => x.Id == modelId);

                if (modelId == 0)
                {
                    var DefaultPeriodId = whris.MstPeriods.ToList().LastOrDefault()?.Id ?? 0;

                    var maxDTRNumber = Int64.Parse(whris.TrnDTRs.Max(x => x.DTRNumber).Substring(5, 6));

                    var DefaultDTRNumber = DateTime.Now.Year + "-" + (maxDTRNumber + 1000001).ToString().Substring(1, 6);

                    var DefaultPayrollGroupId = whris.MstPayrollGroups.FirstOrDefault()?.Id ?? 0;
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
        public JsonResult GetDTRList(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            if (filter?.Filters != null)
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
        #endregion

        #region DTR Line
        public JsonResult ReadDTRLines(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter, int modelFilterId)
        {
            var result = new DataSourceResult();
            var mappingProfile = new Mapping.MappingProfile<Data.TrnDTRLine, Models.TrnDTRLine>();

            using (whris = new Data.whrisDataContext())
            {
                var data = whris.TrnDTRLines.Where(x => x.DTRId == modelFilterId).ToList();
                var outputData = mappingProfile.mapper.Map<List<Data.TrnDTRLine>, List<Models.TrnDTRLine>>(data);

                result = outputData.AsQueryable().ToDataSourceResult(take, skip, sort, filter);
            }

            return Json(result);
        }
        public JsonResult CreateDTRLine(IEnumerable<Models.TrnDTRLine> models, int modelFilterId)
        {
            return null;
        }
        public JsonResult UpdateDTRLine(IEnumerable<Models.TrnDTRLine> models, int modelFilterId)
        {
            return null;
        }
        public JsonResult DestroyDTRLine(IEnumerable<Models.TrnDTRLine> models, int modelFilterId)
        {
            return null;
        } 
        #endregion

        #region Combo Box
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

        public JsonResult CmbEmployee()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstEmployees
                         select new Models.ComboBox.TrnDTR.CmbEmployee
                         {
                             Id = i.Id,
                             FullName = i.FullName
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CmbShiftCodes()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstShiftCodes
                         select new Models.ComboBox.MstEmployee.CmbEmployeeShiftCode
                         {
                             Id = i.Id,
                             ShiftCode = i.ShiftCode
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CmbDaytype()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstDayTypes
                         select new Models.ComboBox.TrnDTR.CmbDaytype
                         {
                             Id = i.Id,
                             DayType = i.DayType
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CmbDepartment()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstDepartments
                         select new Models.ComboBox.MstEmployee.CmbEmployeeDepartment
                         {
                             Id = i.Id,
                             Department = i.Department
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}