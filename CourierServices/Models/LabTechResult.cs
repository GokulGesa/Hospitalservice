using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class ResultLabTech
    {
        public int resultlabtestID { get; set; }
        public int RegId { get; set; }
        public string PatientID { get; set; }
        public string MrdNo { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string TestID { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
        public string Comment2 { get; set; }
        public DateTime CreateDate { get; set; }
        public string Units { get; set; }
        public string NormalValues { get; set; }
        public string SpecialComments { get; set; }
        public string StartRange { get; set; }
        public string EndRange { get; set; }
        public string Methodology { get; set; }
        public string ActualValue { get; set; }
        public string AdditionalFixedComments { get; set; }
        public string CalculationFormula { get; set; }
        public string CalculationInformation { get; set; }
        public string CalculationUnits { get; set; }
        public string Notes { get; set; }
        public string CriticalValue { get; set; }
        public string DisplayValueText { get; set; }
        public string SampleContainer { get; set; }
        public int ProfilePriority { get; set; }
        public string FreeText { get; set; }
        public string CricticalResult { get; set; }
        public string NormalValuesFiled { get; set; }
        public int Status { get; set; }
        public bool select { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string saveFlag { get; set; }
        public string confirmFlag { get; set; }
        public string CancelFlag { get; set; }
        public string TestCodeRef { get; set; }
        public string Checkflag { get; set; }
        public string TestType { get; set; }
        public string ProfileName { get; set; }
        public string Total { get; set; }
        public string FinalResult { get; set; }
        public string AmmendComment { get; set; }
        public string PreHistory { get; set; }
        public string CalculationPresent { get; set; }
        public string Splcalculation { get; set; }
        public string CalculationType { get; set; }
        public int EditID { get; set; }
        public string AmmendDate { get; set; }

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int TestPriority { get; set; }
        public int SubProfilePriority { get; set; }
        public int PriorityStatus { get; set; }
        public string SampleName { get; set; }
        public int AmendType { get; set; }
        public string PregRefRange { get; set; }
        public int mulTestCount { get; set; }
        public string DisplayDeptName { get; set; }
        public string DisplayProfileName { get; set; }
        public string DisplayMultiTestName { get; set; }
        public string MultiTestName { get; set; }

        public string PatientName { get; set; }
        public string outSrcdTAT { get; set; }
        public string OutSrcdToName { get; set; }
        public string OustSrced { get; set; }
        public string HistoryDate { get; set;}
    }
}