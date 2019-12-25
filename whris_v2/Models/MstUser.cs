using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace whris_v2.Models
{
    public class MstUser
    {
        public Int64 Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public Boolean IsLocked { get; set; }
        public Int64 EntryUserId { get; set; }
        public DateTime EntryDateTime { get; set; }
        public Int64 UpdateUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}