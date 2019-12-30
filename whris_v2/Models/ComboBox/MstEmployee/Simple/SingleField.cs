using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace whris_v2.Models.ComboBox.MstEmployee.Simple
{
    public class SexModel
    {
        public string Sex { get; set; }
    }
    public static class CmbSex
    {
        public static List<SexModel> Sex
        {
            get
            {
                return new List<SexModel>()
                {
                    new SexModel(){ Sex = "MALE" },
                    new SexModel(){ Sex = "FEMALE" }
                };
            }
        }
    }

    public class CivilStatusModel
    {
        public string CivilStatus { get; set; }
    }

    public static class CmbCivilStatus
    {
        public static List<CivilStatusModel> CivilStatus
        {
            get
            {
                return new List<CivilStatusModel>
                {
                    new CivilStatusModel(){ CivilStatus = "SINGLE" },
                    new CivilStatusModel(){ CivilStatus = "MARRIED" },
                    new CivilStatusModel(){ CivilStatus = "SEPARATED" },
                    new CivilStatusModel(){ CivilStatus = "WIDOW" }

                };
            }
        }
    }

    public class HDMFTypeModel 
    {
        public string HDMFType { get; set; }
    }

    public static class CmbHDMFType 
    {
        public static List<HDMFTypeModel> HDMFType 
        {
            get 
            {
                return new List<HDMFTypeModel>
                {
                    new HDMFTypeModel(){ HDMFType = "Percentage" },
                    new HDMFTypeModel(){ HDMFType = "Value" }
                };
            }
        }
    }

    public class TaxTableModel
    {
        public string TaxTable { get; set; }
    }

    public static class CmbTaxTable
    {
        public static List<TaxTableModel> TaxTable
        {
            get
            {
                return new List<TaxTableModel>
                {
                    new TaxTableModel(){ TaxTable = "Semi-Monthly" },
                    new TaxTableModel(){ TaxTable = "Monthly" }
                };
            }
        }
    }
}