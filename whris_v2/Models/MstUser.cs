using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace whris_v2.Models
{
    public class MstUser
    {
        public Int64 Id { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Fullname")]
        public string FullName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public Boolean IsLocked { get; set; }
        public Int64 EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public Int64 UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}