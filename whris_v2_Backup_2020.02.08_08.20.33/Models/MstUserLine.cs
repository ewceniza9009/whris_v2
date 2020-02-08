using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace whris_v2.Models
{
    public class MstUserLine
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FormId { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanLock { get; set; }
        public bool CanUnlock { get; set; }
        public bool CanPreview { get; set; }
        public bool CanPrint { get; set; }
        public bool CanView { get; set; }
    }
}