using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class TestCodeandPriority
    {
        public int id { get; set; }
        public string MrdNo { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string SampleContainer { get; set; }
        public double TestPriority { get; set; }
        public double DeptPriority { get; set; }
        public double SubProfilePriority { get; set; }
        public double ProfilePriority { get; set; }
        public double ReportPriority { get; set; }
        public string commonParagraph { get; set; }
        public string UrineCulture { get; set; }
        public int Status { get; set; }
        public int TestType { get; set; }
        public string AcceptAll { get; set; }
        public string Accept { get; set; }
        public string RejectAll { get; set; }
        public string Reject { get; set; }
        //Region for multi
        public int MultiComponentId { get; set; }
        public string MultiElementName { get; set; }
        public string MultiCalculation { get; set; }
        public string MultiResult { get; set; }
        public string MultiNotes { get; set; }
        public string MultiComments { get; set; }
        public string MultiActualValue { get; set; }
        public string MultiNormalValues { get; set; }
        public string MultiUnits { get; set; }
        public string MultiDisplayValue { get; set; }
        public string Color { get; set; }
        public string CommentsType { get; set; }
        public string CriticalLow { get; set; }
        public string CriticalHigh { get; set; }
        public string ReferenceLow { get; set; }
        public string ReferenceHigh { get; set; }
        public int RegID { get; set; }
        public int Priority { get; set; }
        public string Methodology { get; set; }
    }
}
