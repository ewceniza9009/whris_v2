using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace whris_v2.Models
{
    public class MstEmployee
    {
        #region List
        public int Id { get; set; }
        public string IdNumber { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        #endregion

        public int BiometricIdNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ExtensionName { get; set; }
        public string Address { get; set; }
        public int ZipCodeId { get; set; }
        public string PhoneNumber { get; set; }
        public string CellphoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public int? PlaceOfBirthZipCodeId { get; set; }
        public DateTime DateHired { get; set; }
        public DateTime? DateResigned { get; set; }
        public string Sex { get; set; }
        public string CivilStatus { get; set; }
        public int CitizenshipId { get; set; }
        public int ReligionId { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string GSISNumber { get; set; }
        public string SSSNumber { get; set; }
        public string HDMFNumber { get; set; }
        public string PHICNumber { get; set; }
        public string TIN { get; set; }
        public int? TaxCodeId { get; set; }
        public string ATMAccountNumber { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DivisionId { get; set; }
        public int? PositionId { get; set; }
        public int? PayrollGroupId { get; set; }
        public int? AccountId { get; set; }
        public int? PayrollTypeId { get; set; }
        public int? ShiftCodeId { get; set; }
        public int? FixNumberOfDays { get; set; }
        public int? FixNumberOfHours { get; set; }
        public decimal? MonthlyRate { get; set; }
        public decimal? PayrollRate { get; set; }
        public decimal? DailyRate { get; set; }
        public decimal? AbsentDailyRate { get; set; }
        public decimal? HourlyRate { get; set; }
        public decimal? NightHourlyRate { get; set; }
        public decimal? OvertimeHourlyRate { get; set; }
        public decimal? OvertimeNightHourlyRate { get; set; }
        public decimal? TardyHourlyRate { get; set; }
        public int? EntryUserId { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public bool? IsLocked { get; set; }
        public string TaxTable { get; set; }
        public decimal? HDMFAddOn { get; set; }
        public decimal? SSSAddOn { get; set; }
        public string HDMFType { get; set; }
        public bool? SSSIsGrossAmount { get; set; }
        public bool? IsMinimumWageEarner { get; set; }
        public int Capacity { get; set; }
    }
}