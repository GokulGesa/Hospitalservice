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

    }
}