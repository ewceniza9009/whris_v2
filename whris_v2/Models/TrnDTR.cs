using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace whris_v2.Models
{
    public class TrnDTR
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Period")]
        public int PeriodId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        [Display(Name = "DTR Number")]
        public string DTRNumber { get; set; }
        [Required]
        [Display(Name = "DTR Date")]
        public DateTime DTRDate { get; set; }
        [Required]
        [Display(Name = "Payroll Group")]
        public int PayrollGroupId { get; set; }
        [Required]
        [Display(Name = "Date Start")]
        public DateTime DateStart { get; set; }
        [Required]
        [Display(Name = "Date End")]
        public DateTime DateEnd { get; set; }
        [Display(Name = "Overt Time Id")]
        public int? OvertTimeId { get; set; }
        [Display(Name = "Leave Application")]
        public int? LeaveApplicationI { get; set; }
        [Display(Name = "Change Shift Id")]
        public int? ChangeShiftId { get; set; }
        [MaxLength]
        [Required]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
        [Display(Name = "Prepared By")]
        public int? PreparedBy { get; set; }
        [Display(Name = "Checked By")]
        public int? CheckedBy { get; set; }
        [Display(Name = "Approved By")]
        public int? ApprovedBy { get; set; }
        [Display(Name = "Entry User")]
        public int EntryUserId { get; set; }
        [Display(Name = "Entry Date")]
        public DateTime EntryDateTime { get; set; }
        [Display(Name = "Update User")]
        public int UpdateUserId { get; set; }
        [Display(Name = "Update Date")]
        public DateTime UpdateDateTime { get; set; }
        [Display(Name = "Locked")]
        public bool IsLocked { get; set; }
    }

}