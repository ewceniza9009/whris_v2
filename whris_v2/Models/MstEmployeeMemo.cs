using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace whris_v2.Models
{
    public class MstEmployeeMemo
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime MemoDate { get; set; }
        public string MemoSubject { get; set; }
        public string MemoContent { get; set; }
        public int PreparedBy { get; set; }
        public int ApprovedBy { get; set; }
    }
}