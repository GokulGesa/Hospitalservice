using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class ChildTestList
    {
        public int Sno { get; set; }
        public int TestID { get; set; }
        public int ProfileID { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string DepartmentName { get; set; }
        public Boolean NumericOrText { get; set; }

        public int Priority { get; set; }
        public string Methodology { get; set; }
        public string SampleContainer { get; set; }
        public string UnitMeasurementNumeric { get; set; }
        public string UnitMesurementFreeText { get; set; }
        public string TableRequiredPrint { get; set; }
        public string DefaultValues { get; set; }
        public string GenderMale { get; set; }
        public string GenderFemale { get; set; }
        public string Pregnancyrefrange { get; set; }
        public string Multiplecomponents { get; set; }
        public string AdditionalFixedComments { get; set; }
        public string LowerCriticalValue { get; set; }
        public string UpperCriticalValue { get; set; }
        public string OtherCriticalReport { get; set; }
        public string AgewiseCriticalValue { get; set; }
        public string AgewiseReferenceRange { get; set; }
        public string units { get; set; }
        public string TurnAroundTime { get; set; }
        public string RequiredBiospyTestNumber { get; set; }
        public string RequiredSamples { get; set; }
        public string PatientPreparation { get; set; }
        public string ExpectedResultDate { get; set; }
        public double Amount { get; set; }
        public string Finaloutput { get; set; }
        public string TestbasedDiscount { get; set; }
        public string Outsourced { get; set; }
        public string CreateDate { get; set; }
        
        public string AlternativeSample { get; set; }
        public string DisplayName { get; set; }
        public string ValidDate { get; set; }
        public string TestSchedule { get; set; }
        public string cutOffTime { get; set; }
        public string TestInformation { get; set; }
        public string CalculationPresent { get; set; }
        public int ActiveStatus { get; set; }
        public string InstrumentReagent { get; set; }
        public string commonParagraph { get; set; }
        public string UrineCulture { get; set; }
        public string AgewiseSexReferenceValue { get; set; }
        public int ProfilePriority { get; set; }
        public int TestPriority { get; set; }
    }
}