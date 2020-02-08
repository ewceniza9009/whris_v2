using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace whris_v2.Models
{
    public class TrnDTRLine
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "DTR")]
        public int DTRId { get; set; }
        [Required]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name = "Shift Code")]
        public int ShiftCodeId { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Time In1")]
        public DateTime? TimeIn1 { get; set; }
        [Display(Name = "Time Out1")]
        public DateTime? TimeOut1 { get; set; }
        [Display(Name = "Time In2")]
        public DateTime? TimeIn2 { get; set; }
        [Display(Name = "Time Out2")]
        public DateTime? TimeOut2 { get; set; }
        [Required]
        [Display(Name = "Official Business")]
        public bool OfficialBusiness { get; set; }
        [Required]
        [Display(Name = "On Leave")]
        public bool OnLeave { get; set; }
        [Required]
        [Display(Name = "Absent")]
        public bool Absent { get; set; }
        [Required]
        [Display(Name = "Halfday Absent")]
        public bool HalfdayAbsent { get; set; }
        [Required]
        [Display(Name = "Regular Hours")]
        public decimal RegularHours { get; set; }
        [Required]
        [Display(Name = "Night Hours")]
        public decimal NightHours { get; set; }
        [Required]
        [Display(Name = "Overtime Hours")]
        public decimal OvertimeHours { get; set; }
        [Required]
        [Display(Name = "Overtime Night Hours")]
        public decimal OvertimeNightHours { get; set; }
        [Required]
        [Display(Name = "Gross Total Hours")]
        public decimal GrossTotalHours { get; set; }
        [Required]
        [Display(Name = "Tardy Late Hours")]
        public decimal TardyLateHours { get; set; }
        [Required]
        [Display(Name = "Tardy Undertime Hours")]
        public decimal TardyUndertimeHours { get; set; }
        [Required]
        [Display(Name = "Net Total Hours")]
        public decimal NetTotalHours { get; set; }
        [Required]
        [Display(Name = "Day Type Id")]
        public int DayTypeId { get; set; }
        [Required]
        [Display(Name = "Rest Day")]
        public bool RestDay { get; set; }
        [Required]
        [Display(Name = "Day Multiplier")]
        public decimal DayMultiplier { get; set; }
        [Required]
        [Display(Name = "Rate Per Hour")]
        public decimal RatePerHour { get; set; }
        [Required]
        [Display(Name = "Rate Per Night Hour")]
        public decimal RatePerNightHour { get; set; }
        [Required]
        [Display(Name = "Rate Per Overtime Hour")]
        public decimal RatePerOvertimeHour { get; set; }
        [Required]
        [Display(Name = "Rate Per Overtime Night Hour")]
        public decimal RatePerOvertimeNightHour { get; set; }
        [Required]
        [Display(Name = "Regular Amount")]
        public decimal RegularAmount { get; set; }
        [Required]
        [Display(Name = "Night Amount")]
        public decimal NightAmount { get; set; }
        [Required]
        [Display(Name = "Overtime Amount")]
        public decimal OvertimeAmount { get; set; }
        [Required]
        [Display(Name = "Overtime Night Amount")]
        public decimal OvertimeNightAmount { get; set; }
        [Required]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }
        [Required]
        [Display(Name = "Rate Per Hour Tardy")]
        public decimal RatePerHourTardy { get; set; }
        [Required]
        [Display(Name = "Rate Per Absent Day")]
        public decimal RatePerAbsentDay { get; set; }
        [Required]
        [Display(Name = "Tardy Amount")]
        public decimal TardyAmount { get; set; }
        [Required]
        [Display(Name = "Absent Amount")]
        public decimal AbsentAmount { get; set; }
        [Required]
        [Display(Name = "Net Amount")]
        public decimal NetAmount { get; set; }
    }
}