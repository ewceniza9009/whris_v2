﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Kendo.DynamicLinq;

namespace whris_v2.Controllers
{
    public class MstEmployeeController : Controller
    {
        public Data.whrisDataContext whris;

        public JsonResult GetEmployeeList(int take, int skip, IEnumerable<Sort> sort, Kendo.DynamicLinq.Filter filter)
        {
            whris = new Data.whrisDataContext();

            //var data = whris.MstEmployees.ToDataSourceResult(take, skip, sort, filter);

            //var mappingProfile = new Mapping.MappingProfile<Data.MstEmployee, Models.MstEmployee>();
            //var dataConverted = mappingProfile.mapper.Map<IEnumerable<Data.MstEmployee>, IEnumerable<Models.MstEmployee>>(data.Data.Cast<Data.MstEmployee>());

            //var result = new Kendo.DynamicLinq.DataSourceResult()
            //{
            //    Data = dataConverted,
            //    Total = data.Total,
            //    Aggregates = data.Aggregates
            //};

            //var result = (from emp in whris.MstEmployees
            //              join pos in whris.MstPositions on emp.PositionId equals pos.Id
            //              orderby emp.FullName
            //              select new Models.MstEmployee
            //              {
            //                  Id = emp.Id,
            //                  IdNumber = emp.IdNumber,
            //                  FullName = emp.FullName,
            //                  Position = pos.Position,
            //                  IsLocked = emp.IsLocked
            //              }).ToDataSourceResult(take, skip, sort, filter);

            var result = whris.MstEmployees
                .Select(x => new Models.MstEmployee
                {
                    Id = x.Id,
                    IdNumber = x.IdNumber,
                    FullName = x.FullName,
                    Position = x.MstPosition.Position,
                    IsLocked = x.IsLocked
                })
                .ToDataSourceResult(take, skip, sort, filter);

            return Json(result);
        }
        public ActionResult MstEmployeeDetail(int modelId)
        {
            var result = new Models.MstEmployee();
            var data = new Data.MstEmployee();

            var mappingProfile = new Mapping.MappingProfile<Data.MstEmployee, Models.MstEmployee>();

            using (whris = new Data.whrisDataContext())
            {
                data = whris.MstEmployees.Where(x => x.Id == modelId).FirstOrDefault();

                if (modelId == 0)
                {
                    var DefaultZipCodeId = whris.MstZipCodes.FirstOrDefault().Id;
                    var DefaultCitizenshipId = whris.MstCitizenships.FirstOrDefault().Id;
                    var DefaultReligionId = whris.MstReligions.FirstOrDefault().Id;
                    var DefaultTaxCodeId = whris.MstTaxCodes.FirstOrDefault().Id;
                    var DefaultCompanyId = whris.MstCompanies.FirstOrDefault().Id;
                    var DefaultBranchId = whris.MstBranches.FirstOrDefault().Id;
                    var DefaultDepartmentId = whris.MstDepartments.FirstOrDefault().Id;
                    var DefaultDivisionId = whris.MstDivisions.FirstOrDefault().Id;
                    var DefaultPositionId = whris.MstPositions.FirstOrDefault().Id;
                    var DefaultPayrollGroupId = whris.MstPayrollGroups.FirstOrDefault().Id;
                    var DefaultAccountId = whris.MstAccounts.FirstOrDefault().Id;
                    var DefaultPayrollTypeId = whris.MstPayrollTypes.FirstOrDefault().Id;
                    var DefaultShiftCodeId = whris.MstShiftCodes.FirstOrDefault().Id;

                    data = new Data.MstEmployee
                    {
                        Id = 0,
                        IdNumber = "NA",
                        BiometricIdNumber = 0,
                        LastName = "NA",
                        FirstName = "NA",
                        MiddleName = "NA",
                        ExtensionName = "NA",
                        FullName = "NA",
                        Address = "NA",
                        ZipCodeId = DefaultZipCodeId,
                        PhoneNumber = "NA",
                        CellphoneNumber = "NA",
                        EmailAddress = "NA",
                        DateOfBirth = DateTime.Now,
                        PlaceOfBirth = "NA",
                        PlaceOfBirthZipCodeId = DefaultZipCodeId,
                        DateHired = DateTime.Now,
                        DateResigned = DateTime.Now,
                        Sex = "Male",
                        CivilStatus = "SINGLE",
                        CitizenshipId = DefaultCitizenshipId,
                        ReligionId = DefaultReligionId,
                        Height = 0,
                        Weight = 0,
                        GSISNumber = "NA",
                        SSSNumber = "NA",
                        HDMFNumber = "NA",
                        PHICNumber = "NA",
                        TIN = "NA",
                        TaxCodeId = DefaultTaxCodeId,
                        ATMAccountNumber = "NA",
                        CompanyId = DefaultCompanyId,
                        BranchId = DefaultBranchId,
                        DepartmentId = DefaultDepartmentId,
                        DivisionId = DefaultDivisionId,
                        PositionId = DefaultPositionId,
                        PayrollGroupId = DefaultPayrollGroupId,
                        AccountId = DefaultAccountId,
                        PayrollTypeId = DefaultPayrollTypeId,
                        ShiftCodeId = DefaultShiftCodeId,
                        FixNumberOfDays = 0,
                        FixNumberOfHours = 0,
                        MonthlyRate = 0,
                        PayrollRate = 0,
                        DailyRate = 0,
                        AbsentDailyRate = 0,
                        HourlyRate = 0,
                        NightHourlyRate = 0,
                        OvertimeHourlyRate = 0,
                        OvertimeNightHourlyRate = 0,
                        TardyHourlyRate = 0,
                        EntryUserId = 1,
                        EntryDateTime = DateTime.Now,
                        UpdateUserId = 1,
                        UpdateDateTime = DateTime.Now,
                        IsLocked = false,
                        TaxTable = "NA",
                        HDMFAddOn = 0,
                        SSSAddOn = 0,
                        HDMFType = "NA",
                        SSSIsGrossAmount = false,
                        IsMinimumWageEarner = false
                    };
                }

                result = mappingProfile.mapper.Map<Data.MstEmployee, Models.MstEmployee>(data);
            }

            return View(result);
        }
        [HttpPost]
        public ActionResult SaveEmployee(Models.MstEmployee model)
        {
            var result = new Data.MstEmployee();
            var mappingProfile = new Mapping.MappingProfile<Data.MstEmployee, Models.MstEmployee>();

            using (whris = new Data.whrisDataContext())
            {
                if (model.Id != 0)
                {
                    result = whris.MstEmployees.Where(x => x.Id == model.Id).FirstOrDefault();

                    result = mappingProfile.mapper.Map<Models.MstEmployee, Data.MstEmployee>(model);

                    result.EntryUserId = 1;
                    result.EntryDateTime = DateTime.Now;
                    result.UpdateUserId = 1;
                    result.UpdateDateTime = DateTime.Now;
                }
                else
                {
                    result = mappingProfile.mapper.Map<Models.MstEmployee, Data.MstEmployee>(model);

                    result.EntryUserId = 1;
                    result.EntryDateTime = DateTime.Now;
                    result.UpdateUserId = 1;
                    result.UpdateDateTime = DateTime.Now;

                    whris.MstEmployees.InsertOnSubmit(result);
                }

                whris.SubmitChanges();
            }

            return View(model);
        }

        #region Combobox
        #region Employee Details Tab1
        public JsonResult CmbZipCode()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstZipCodes
                         orderby i.ZipCode ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeeZipCode
                         {
                             Id = i.Id,
                             ZipCode = i.ZipCode,
                             Location = i.Location
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CmbPlaceOfBirthZipCode()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstZipCodes
                         orderby i.ZipCode ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeeZipCode
                         {
                             Id = i.Id,
                             ZipCode = i.ZipCode,
                             Location = i.Location
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CmbCitizenship()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstCitizenships
                         orderby i.Citizenship ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeeCitizenship
                         {
                             Id = i.Id,
                             Citizenship = i.Citizenship,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CmbReligion()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstReligions
                         orderby i.Religion ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeeReligion
                         {
                             Id = i.Id,
                             Religion = i.Religion,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Employee Details Tab2
        #region Column1
        public JsonResult CmbTaxCode()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstTaxCodes
                         orderby i.TaxCode ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeeTaxCode
                         {
                             Id = i.Id,
                             TaxCode = i.TaxCode,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CmbCompany()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstCompanies
                         orderby i.Company ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeeCompany
                         {
                             Id = i.Id,
                             Company = i.Company,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CmbBranch()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstBranches
                         orderby i.Branch ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeeBranch
                         {
                             Id = i.Id,
                             Branch = i.Branch,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CmbDepartment()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstDepartments
                         orderby i.Department ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeeDepartment
                         {
                             Id = i.Id,
                             Department = i.Department,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CmbPosition()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstPositions
                         orderby i.Position ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeePosition
                         {
                             Id = i.Id,
                             Position = i.Position,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CmbDivision()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstDivisions
                         orderby i.Division ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeeDivison
                         {
                             Id = i.Id,
                             Division = i.Division,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Column2
        public JsonResult CmbPayrollGroup()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstPayrollGroups
                         orderby i.PayrollGroup ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeePayrollGroup
                         {
                             Id = i.Id,
                             PayrollGroup = i.PayrollGroup,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CmbPayrollType()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstPayrollTypes
                         orderby i.PayrollType ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeePayrollType
                         {
                             Id = i.Id,
                             PayrollType = i.PayrollType,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CmbGLAccount()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstAccounts
                         orderby i.Account ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeeGLAccount
                         {
                             Id = i.Id,
                             Account = i.Account,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CmbShiftCode()
        {
            whris = new Data.whrisDataContext();

            var result = from i in whris.MstShiftCodes
                         orderby i.ShiftCode ascending
                         select new Models.ComboBox.MstEmployee.CmbEmployeeShiftCode
                         {
                             Id = i.Id,
                             ShiftCode = i.ShiftCode,
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #endregion

        // GET: MstEmployee
        public ActionResult Index()
        {
            return View();
        }
    }
}