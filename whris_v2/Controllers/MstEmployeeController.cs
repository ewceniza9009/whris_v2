using System;
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

            var data = whris.MstEmployees.ToDataSourceResult(take, skip, sort, filter);

            var mappingProfile = new Mapping.MappingProfile<Data.MstEmployee, Models.MstEmployee>();
            var dataConverted = mappingProfile.mapper.Map<IEnumerable<Data.MstEmployee>, IEnumerable<Models.MstEmployee>>(data.Data.Cast<Data.MstEmployee>());

            var result = new Kendo.DynamicLinq.DataSourceResult()
            {
                Data = dataConverted,
                Total = data.Total,
                Aggregates = data.Aggregates
            };

            return Json(result);
        }
        public ActionResult GetEmployee(int empId)
        {
            var result = new Models.MstEmployee();
            var data = new Data.MstEmployee();

            var mappingProfile = new Mapping.MappingProfile<Data.MstEmployee, Models.MstEmployee>();

            using (whris = new Data.whrisDataContext())
            {
                data = whris.MstEmployees.Where(x => x.Id == empId).FirstOrDefault();

                if (empId == 0)
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

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveEmployee(Models.MstEmployee empClient)
        {
            var result = new Data.MstEmployee();
            var mappingProfile = new Mapping.MappingProfile<Data.MstEmployee, Models.MstEmployee>();

            using (whris = new Data.whrisDataContext())
            {
                if (empClient.Id != 0)
                {
                    result = whris.MstEmployees.Where(x => x.Id == empClient.Id).FirstOrDefault();

                    result = mappingProfile.mapper.Map<Models.MstEmployee, Data.MstEmployee>(empClient);

                    result.EntryUserId = 1;
                    result.EntryDateTime = DateTime.Now;
                    result.UpdateUserId = 1;
                    result.UpdateDateTime = DateTime.Now;
                }
                else
                {
                    result = mappingProfile.mapper.Map<Models.MstEmployee, Data.MstEmployee>(empClient);

                    result.EntryUserId = 1;
                    result.EntryDateTime = DateTime.Now;
                    result.UpdateUserId = 1;
                    result.UpdateDateTime = DateTime.Now;

                    whris.MstEmployees.InsertOnSubmit(result);
                }

                whris.SubmitChanges();
            }

            return View(empClient);
        }

        // GET: MstEmployee
        public ActionResult Index()
        {
            return View();
        }
    }
}