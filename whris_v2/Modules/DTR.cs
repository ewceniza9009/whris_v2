﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using whris_v2.Data;

namespace whris_v2.Modules
{
    public class DTR
    {
        Data.whrisDataContext whris;
        public void ProcessDTRDate(int dtrId, int? payrollGroupId, int? departmentId, int? employeeId, DateTime? startDate, DateTime? endDate)
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

                if (rsEmployee.Count > 0)
                {
                    if (employeeId == null)
                    {
                        if (departmentId == null)
                        {
                            var deleteLine = whris.TrnDTRLines.Where(x =>
                                x.DTRId == dtrId && x.Date.Date >= startDate && x.Date.Date <= endDate);
                        }
                        else
                        {
                            var deleteLine = whris.TrnDTRLines.Where(x =>
                                x.DTRId == dtrId && x.MstEmployee.DepartmentId == departmentId && x.Date.Date >= startDate && x.Date.Date <= endDate);
                        }
                    }
                    else
                    {
                        var deleteLine = whris.TrnDTRLines.Where(x =>
                            x.DTRId == dtrId && x.EmployeeId == employeeId && x.Date.Date >= startDate && x.Date.Date <= endDate);
                    }
                }

                foreach (var emp in rsEmployee)
                {
                    foreach (var d in EachDay(startDate ?? DateTime.Now.Date, endDate ?? DateTime.Now.Date))
                    {
                        var newDTRLine = new Data.TrnDTRLine
                        {
                            DTRId = dtrId,
                            EmployeeId = emp.Id,
                            Date = d,
                            ShiftCodeId = 0,
                            TimeIn1 = null,
                            TimeOut1 = null,
                            TimeIn2 = null,
                            TimeOut2 = null,
                            OfficialBusiness = false,
                            OnLeave = false,
                            Absent = false,
                            HalfdayAbsent = false,
                            RegularHours = 0,
                            NightHours = 0,
                            OvertimeHours = 0,
                            OvertimeNightHours = 0,
                            GrossTotalHours = 0,
                            TardyLateHours = 0,
                            TardyUndertimeHours = 0,
                            NetTotalHours = 0,
                            DayTypeId = 1,
                            RestDay = false,
                            DayMultiplier = 1,
                            RatePerHour = 0,
                            RatePerNightHour = 0,
                            RatePerOvertimeHour = 0,
                            RatePerOvertimeNightHour = 0,
                            RegularAmount = 0,
                            NightAmount = 0,
                            OvertimeAmount = 0,
                            OvertimeNightAmount = 0,
                            TotalAmount = 0,
                            RatePerHourTardy = 0,
                            RatePerAbsentDay = 0,
                            TardyAmount = 0,
                            AbsentAmount = 0,
                            NetAmount = 0
                        };

                        whris.TrnDTRLines.InsertOnSubmit(newDTRLine);
                        whris.SubmitChanges();
                    }
                }
            }
        }
        public int? ComputeShiftCode2(Models.TrnDTRLine rsDTRLine)
        {
            using (var ctx = new Data.whrisDataContext())
            {
                int? changeShiftId = null;
                int? shiftCodeId = null;

                int? currentShiftCodeId = rsDTRLine.ShiftCodeId;

                changeShiftId = ctx.TrnDTRs.FirstOrDefault(x => x.Id == rsDTRLine.Id)?.ChangeShiftId;

                if (changeShiftId != null && rsDTRLine?.EmployeeId != null)
                {
                    shiftCodeId = ctx.TrnChangeShiftLines
                        .FirstOrDefault(x => x.ChangeShiftId == changeShiftId &&
                                             x.EmployeeId == rsDTRLine.EmployeeId &&
                                             x.Date.Date == rsDTRLine.Date.Date)
                        ?.ShiftCodeId;

                    if (shiftCodeId > 0)
                    {
                        return shiftCodeId;
                    }
                    else
                    {
                        return currentShiftCodeId;
                    }
                }
                else
                {
                    return currentShiftCodeId;
                }
            }
        }
        public bool? ComputeRestDay(Models.TrnDTRLine rsDTRLine)
        {
            bool? bolReturn = null;

            using (var ctx = new Data.whrisDataContext())
            {
                var rsShiftCodeDay = ctx.MstShiftCodeDays
                    .Where(x => x.ShiftCodeId == rsDTRLine.ShiftCodeId &&
                        string.Equals(x.Day, rsDTRLine.Date.ToString("dddd"),
                            StringComparison.CurrentCultureIgnoreCase))
                    ?.FirstOrDefault();

                if (rsShiftCodeDay != null)
                {
                    bolReturn = rsShiftCodeDay.RestDay;
                }
                else
                {
                    bolReturn = false;
                }
            }

            return bolReturn;
        }
        public bool? ComputeOnLeave(Models.TrnDTRLine rsDTRLine)
        {
            int? changeShiftId = null;
            bool? bolReturn = null;

            using (var ctx = new Data.whrisDataContext())
            {
                var leaveApplicationId = ctx.TrnDTRs
                    .FirstOrDefault(x => x.Id == rsDTRLine.DTRId)
                    ?.LeaveApplicationId;

                if (leaveApplicationId != null && rsDTRLine?.EmployeeId != null)
                {
                    var rsLeaveApplication = ctx.TrnLeaveApplicationLines
                        .Where(x => x.LeaveApplicationId == leaveApplicationId &&
                            x.EmployeeId == rsDTRLine.EmployeeId &&
                            x.Date.Date == rsDTRLine.Date.Date);
                    bolReturn = rsLeaveApplication.Any();
                }
            }

            return bolReturn;
        }
        public bool? ComputeAbsent(Models.TrnDTRLine rsDTRLine)
        {
            bool? bolReturn = null;

            using (var ctx = new Data.whrisDataContext())
            {
                if (rsDTRLine.TimeIn1 != null &&
                    rsDTRLine.TimeOut1 != null &&
                    rsDTRLine.TimeIn2 != null &&
                    rsDTRLine.TimeOut2 != null &&
                    rsDTRLine.OfficialBusiness == false &&
                    rsDTRLine.OnLeave == false &&
                    rsDTRLine.RestDay == false &&
                    rsDTRLine.DayTypeId == 1)
                {
                    bolReturn = false;
                }

                if (rsDTRLine.TimeIn1 != null &&
                    rsDTRLine.TimeOut1 != null &&
                    rsDTRLine.TimeIn2 != null &&
                    rsDTRLine.TimeOut2 != null &&
                    rsDTRLine.OfficialBusiness == true &&
                    rsDTRLine.OnLeave == false &&
                    rsDTRLine.RestDay == false &&
                    rsDTRLine.DayTypeId == 1)
                {
                    var withPay = ctx.TrnLeaveApplicationLines
                        .Where(x => x.EmployeeId == rsDTRLine.EmployeeId &&
                            x.Date.Date == rsDTRLine.Date.Date)
                        .FirstOrDefault()
                        .WithPay;
                    if (!withPay)
                    {
                        bolReturn = true;
                    }
                    else
                    {
                        bolReturn = false;
                    }
                }

                if (rsDTRLine.TimeIn1 != null &&
                    rsDTRLine.TimeOut1 != null &&
                    rsDTRLine.TimeIn2 != null &&
                    rsDTRLine.TimeOut2 != null &&
                    rsDTRLine.OfficialBusiness == false &&
                    rsDTRLine.OnLeave == false &&
                    rsDTRLine.RestDay == false &&
                    rsDTRLine.DayTypeId == 1)
                {
                    bolReturn = false;

                    int? payrollTypeId = ctx.MstEmployees.Where(x => x.Id == rsDTRLine.EmployeeId).FirstOrDefault().PayrollTypeId;

                    if (payrollTypeId == 2)
                    {
                        bool withAbsentInFixed = ctx.MstDayTypeDays.Where(x => x.Date.Date == rsDTRLine.Date.Date).FirstOrDefault().WithAbsentInFixed;

                        if (withAbsentInFixed)
                        {
                            bolReturn = true;
                        }
                    }
                }
                else
                {
                    bolReturn = false;
                }
            }

            return bolReturn;
        }
        public int? ComputeDayType(Models.TrnDTRLine rsDTRLine)
        {
            int? intDayType = null;
            int? branchId = null;

            using (var ctx = new Data.whrisDataContext())
            {
                branchId = ctx.MstEmployees.Where(x => x.Id == rsDTRLine.EmployeeId)?.FirstOrDefault()?.BranchId;

                var rsDayTypeDay = ctx.MstDayTypeDays
                    .Where(x => x.Date.Date == rsDTRLine.Date.Date &&
                        x.BranchId == branchId)
                    ?.FirstOrDefault();

                intDayType = 1;

                if (rsDayTypeDay != null)
                {
                    intDayType = rsDayTypeDay.DayTypeId;
                }

                return intDayType;
            }
        }
        public decimal? ComputeRegularHours(Models.TrnDTRLine rsDTRLine)
        {
            decimal? dblNumberOfHours = null;
            bool? boolReturn = null;

            using (var ctx = new Data.whrisDataContext())
            {
                var rsShiftCodeDay = ctx.MstShiftCodeDays
                    .Where(x => x.ShiftCodeId == rsDTRLine.ShiftCodeId &&
                        x.Day.ToUpper() == rsDTRLine.Date.ToString("dddd").ToUpper())
                    ?.FirstOrDefault();

                if (rsShiftCodeDay != null)
                {
                    if (rsShiftCodeDay.TimeIn1 != null &&
                        rsShiftCodeDay.TimeOut1 != null &&
                        rsShiftCodeDay.TimeOut2 != null &&
                        rsShiftCodeDay.TimeOut2 != null)
                    {
                        if (rsShiftCodeDay.TimeIn1 != null ||
                            rsShiftCodeDay.TimeOut1 != null ||
                            rsShiftCodeDay.TimeOut2 != null ||
                            rsShiftCodeDay.TimeOut2 != null)
                        {
                            dblNumberOfHours = rsShiftCodeDay.NumberOfHours;
                        }
                    }
                }
                else
                {
                    if (rsShiftCodeDay.TimeIn1 != null ||
                       rsShiftCodeDay.TimeOut2 != null)
                    {
                        dblNumberOfHours = rsShiftCodeDay.NumberOfHours;
                    }
                }

                return dblNumberOfHours;
            }
        }
        public decimal? ComputeNightHours(Models.TrnDTRLine rsDTRLine)
        {
            decimal? dblNumberOfHours = null;

            DateTime? nightTimeStart = rsDTRLine.Date.Date.AddHours(22);
            DateTime? nightTimeEnd = rsDTRLine.Date.Date.AddHours(6);

            DateTime? actualNightTimeStart = null;
            DateTime? actualNightTimeEnd = null;
            decimal? nightHours = null;

            dblNumberOfHours = 0;

            if (rsDTRLine.TimeIn1 >= nightTimeStart ||
               rsDTRLine.TimeIn1 < nightTimeStart &&
               rsDTRLine.TimeOut2 > nightTimeStart)
            {
                if (rsDTRLine.TimeIn1 >= nightTimeStart &&
                    rsDTRLine.TimeIn1 > nightTimeEnd)
                {
                    actualNightTimeStart = rsDTRLine.TimeIn1;
                }
                else
                {
                    actualNightTimeStart = nightTimeStart;
                }

                if (rsDTRLine.TimeOut2 <= nightTimeEnd &&
                    rsDTRLine.TimeOut2 > nightTimeStart)
                {
                    actualNightTimeEnd = rsDTRLine.TimeOut2;
                }
                else
                {
                    actualNightTimeEnd = nightTimeEnd;
                }

                dblNumberOfHours = Convert.ToDecimal(((TimeSpan)(actualNightTimeEnd - actualNightTimeStart)).TotalHours);
            }

            using (var ctx = new Data.whrisDataContext())
            {
                nightHours = ctx.MstShiftCodeDays
                    .Where(x => x.ShiftCodeId == rsDTRLine.ShiftCodeId &&
                        x.Day.ToUpper() == rsDTRLine.Date.ToString("dddd").ToUpper())
                    ?.FirstOrDefault()
                    ?.NightHours;

                if (dblNumberOfHours > nightHours)
                {
                    dblNumberOfHours = nightHours;
                }

                if (dblNumberOfHours < 0)
                {
                    dblNumberOfHours = 0m;
                }

                return (decimal?)Math.Round(Convert.ToDecimal(dblNumberOfHours), 0);
            }
        }
        public decimal? ComputeOvertimeHours(Models.TrnDTRLine rsDTRLine) 
        {
            decimal? dblRegHours = null;
            decimal? dblOTHours = null;
            decimal? dblOTLimitHours = null;
            decimal? dblActualOTHours = null;
            decimal? dblActualOTHoursOnIn = null;
            DateTime? datShiftTimeIn1 = null;
            DateTime? datShiftTimeIn2 = null;
            DateTime? datShiftTimeOut1 = null;
            DateTime? datShiftTimeOut2 = null;

            using (var ctx = new Data.whrisDataContext()) 
            {
                int? lngOvertimeId = ctx.TrnDTRs.Where(x => x.Id == rsDTRLine.DTRId)?.FirstOrDefault()?.OvertTimeId;

                dblOTHours = ctx.TrnOverTimeLines
                    .Where(x => x.EmployeeId == rsDTRLine.EmployeeId &&
                        x.OverTimeId == lngOvertimeId &&
                        x.Date.Date == rsDTRLine.Date.Date)
                    ?.FirstOrDefault()
                    ?.OvertimeHours;

                dblOTLimitHours = ctx.TrnOverTimeLines
                    .Where(x => x.EmployeeId == rsDTRLine.EmployeeId &&
                        x.OverTimeId == lngOvertimeId &&
                        x.Date.Date == rsDTRLine.Date.Date)
                    ?.FirstOrDefault()
                    ?.OvertimeLimitHours;

                if (dblOTHours != null) 
                {
                    if (rsDTRLine.TimeOut2 != null) 
                    {
                        if (rsDTRLine.RestDay)
                        {
                            dblRegHours = ctx.MstShiftCodeDays
                                .Where(x => x.ShiftCodeId == rsDTRLine.ShiftCodeId &&
                                    x.Day.ToUpper() == rsDTRLine.Date.ToString("dddd").ToUpper())
                                ?.FirstOrDefault()
                                ?.NumberOfHours;

                            if (rsDTRLine.TimeIn1 != null && rsDTRLine.TimeOut2 != null)
                            {
                                dblActualOTHoursOnIn = Convert.ToDecimal(((TimeSpan)(rsDTRLine.TimeOut2 - rsDTRLine.TimeIn1)).TotalHours);

                                dblActualOTHours = dblActualOTHoursOnIn > 0 ? dblActualOTHoursOnIn : 0;
                            }
                            else
                            {
                                dblActualOTHours = 0;
                            }

                            if (dblActualOTHours > dblRegHours)
                            {
                                dblActualOTHours -= dblRegHours;
                            }
                            else
                            {
                                dblActualOTHours = 0;
                            }

                            if (dblOTHours > dblRegHours)
                            {
                                dblOTHours -= dblRegHours;

                                if (dblOTHours > dblActualOTHours)
                                {
                                    dblOTHours = dblActualOTHours;
                                }
                            }
                            else
                            {
                                dblOTHours = 0;
                            }
                        }
                        else 
                        {
                            var timeIn1 = ctx.MstShiftCodeDays
                                .Where(x => x.ShiftCodeId == rsDTRLine.ShiftCodeId &&
                                    string.Equals(x.Day, rsDTRLine.Date.ToString("dddd"),
                                        StringComparison.CurrentCultureIgnoreCase))
                                ?.FirstOrDefault()
                                ?.TimeIn1
                                ?.ToString();

                            timeIn1 = timeIn1 ?? "12:00:00 AM";

                            datShiftTimeOut2 = DateTime.Parse($"{rsDTRLine.Date.ToShortDateString()} {timeIn1}");

                            var dt12Compare = DateTime.Parse($"{rsDTRLine.Date.ToShortDateString()} 12:00:00 AM");

                            if (datShiftTimeOut2 != dt12Compare)
                            {
                                dblActualOTHours = Convert.ToDecimal(((TimeSpan)(rsDTRLine.TimeOut2 - datShiftTimeOut2)).TotalHours);

                                if (dblActualOTHours <= 0)
                                {
                                    dblActualOTHours = 0;
                                }
                                else 
                                {
                                    if (rsDTRLine.TimeIn1 != null)
                                    {
                                        datShiftTimeIn1 = DateTime.Parse($"{rsDTRLine.Date.ToShortDateString()} {timeIn1}");

                                        if (datShiftTimeIn1 != DateTime.Parse($"{rsDTRLine.Date} 12:00:00 AM"))
                                        {
                                            dblActualOTHoursOnIn =
                                                Convert.ToDecimal(((TimeSpan) (rsDTRLine.TimeIn1 - datShiftTimeIn1))
                                                    .TotalMinutes) / 60;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}