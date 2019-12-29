using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace whris_v2.Models
{
    public class MstEmployee
    {
        public MstEmployee()
        {
            Memos = new List<MstEmployeeMemo>();
        }

        #region List
        public int Id { get; set; }
        [Display(Name = "Id Number")]
        public string IdNumber { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Position")]
        public string Position { get; set; }
        #endregion

        [Display(Name = "Biometric")]
        public int BiometricIdNumber { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle")]
        public string MiddleName { get; set; }
        [Display(Name = "Extension")]
        public string ExtensionName { get; set; }
        public string Address { get; set; }
        [Display(Name = "Zip Code")]
        public int ZipCodeId { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "CP Number")]
        public string CellphoneNumber { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Field can't be empty")]
        [EmailAddress]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string EmailAddress { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Birth Place")]
        public string PlaceOfBirth { get; set; }
        [Display(Name = "Birth Place Zip")]
        public int? PlaceOfBirthZipCodeId { get; set; }
        [Display(Name = "Hired")]
        public DateTime DateHired { get; set; }
        [Display(Name = "Resigned")]
        public DateTime? DateResigned { get; set; }
        public string Sex { get; set; }
        [Display(Name = "Civil Status")]
        public string CivilStatus { get; set; }
        [Display(Name = "Citizenship")]
        public int CitizenshipId { get; set; }
        [Display(Name = "Religion")]
        public int ReligionId { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Height { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Weight { get; set; }
        [Display(Name = "GSIS No.")]
        public string GSISNumber { get; set; }
        [Display(Name = "SSS No.")]
        public string SSSNumber { get; set; }
        [Display(Name = "HDMF No.")]
        public string HDMFNumber { get; set; }
        [Display(Name = "PHIC No.")]
        public string PHICNumber { get; set; }
        public string TIN { get; set; }
        [Display(Name = "Tax Code")]
        public int? TaxCodeId { get; set; }
        [Display(Name = "ATM No.")]
        public string ATMAccountNumber { get; set; }
        [Display(Name = "Company")]
        public int? CompanyId { get; set; }
        [Display(Name = "Branch")]
        public int? BranchId { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        [Display(Name = "Division")]
        public int? DivisionId { get; set; }
        [Display(Name = "Position")]
        public int? PositionId { get; set; }
        [Display(Name = "Payroll Group")]
        public int? PayrollGroupId { get; set; }
        [Display(Name = "Account")]
        public int? AccountId { get; set; }
        [Display(Name = "Payroll Type")]
        public int? PayrollTypeId { get; set; }
        [Display(Name = "Shift Code")]
        public int? ShiftCodeId { get; set; }
        [Display(Name = "Fix No. Days")]
        public int? FixNumberOfDays { get; set; }
        [Display(Name = "Fix No. Hours")]
        public int? FixNumberOfHours { get; set; }
        [Display(Name = "Monthly Rate")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? MonthlyRate { get; set; }
        [Display(Name = "Payroll Rate")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? PayrollRate { get; set; }
        [Display(Name = "Daily Rate")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? DailyRate { get; set; }

        [Display(Name = "Absent Rate")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? AbsentDailyRate { get; set; }
        [Display(Name = "Hourly Rate")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? HourlyRate { get; set; }
        [Display(Name = "N Hourly Rate")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? NightHourlyRate { get; set; }
        [Display(Name = "OT Hourly Rate")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? OvertimeHourlyRate { get; set; }
        [Display(Name = "OTN Hourly Rate")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? OvertimeNightHourlyRate { get; set; }
        [Display(Name = "Tardy Hourly Rate")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? TardyHourlyRate { get; set; }
        public int? EntryUserId { get; set; }
        public DateTime? EntryDateTime { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        [Display(Name = "Locked")]
        public bool IsLocked { get; set; }
        [Display(Name = "Tax Table")]
        public string TaxTable { get; set; }
        [Display(Name = "HDMF Add on")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal HDMFAddOn { get; set; }
        [Display(Name = "SSS Add on")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal SSSAddOn { get; set; }
        [Display(Name = "HDMF Type")]
        public string HDMFType { get; set; }
        [Display(Name = "SSS Gross")]
        public bool SSSIsGrossAmount { get; set; }
        [Display(Name = "Min Wager")]
        public bool IsMinimumWageEarner { get; set; }
        public int Capacity { get; set; }

        public List<MstEmployeeMemo> Memos { get; set; }
    }
}