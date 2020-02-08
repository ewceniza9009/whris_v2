using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace whris_v2.PayrollLogics
{
    public class DTR
    {
        Data.whrisDataContext whris;
        public void ProcessDTRDate(int dtrId, int? payrollGroupId, int? departmentId, int? employeeId) 
        {
            using (whris = new Data.whrisDataContext()) 
            {
                var rsEmployee = new List<Data.MstEmployee>();

                if (employeeId == null)
                {
                    if (departmentId == null)
                    {
                        rsEmployee = whris.MstEmployees
                        .Where(x => x.PayrollGroupId == payrollGroupId)
                        .ToList();
                    }
                    else 
                    {
                        rsEmployee = whris.MstEmployees
                        .Where(x => x.DepartmentId == departmentId && x.PayrollGroupId == payrollGroupId)
                        .ToList();
                    }
                }
                else 
                {
                    rsEmployee = whris.MstEmployees
                        .Where(x => x.Id == employeeId)
                        .ToList();
                }

                foreach (var emp in rsEmployee) 
                {
                
                }
            }
        }
    }
}