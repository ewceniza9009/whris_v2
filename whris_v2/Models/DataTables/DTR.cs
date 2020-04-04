using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace whris_v2.Models.DataTables
{
    public class DTR
    {
        public int No { get; set; }
        public int TMNo { get; set; }
        public int EnNo { get; set; }
        public string Name { get; set; }
        public int INOUT { get; set; }
        public int Mode { get; set; }
        public DateTime DateTime { get; set; }
    }
}