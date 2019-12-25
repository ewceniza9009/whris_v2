using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace whris_v2.Models
{
    public class MstEmployeeShiftCode
    {
        public int Id { get; set; }
        public int ShiftCodeId { get; set; }
        public int EmployeeId { get; set; }
    }
}