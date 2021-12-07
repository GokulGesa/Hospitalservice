using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class SampleContainersByMrdNo
    {
        public int TestID { get; set; }
        public string TestCode { get; set; }
        public int ProfileID { get; set; }
        public string ProfileName { get; set; }
        public string MrdNo { get; set; }
        public string TestName{ get; set; }
        public string SampleContainer { get; set; }
        public int SampleStatus { get; set; }
        public string units { get; set; }
        public string AlternativeSampleAvailable { get; set; }
        public string AlternativeSampleName { get; set; }
        public string Dynamic { get; set; }
        public int IndividualStatus { get; set; }
        public string Catagory_Test_Profile { get; set; }
        public int ProfilePriority { get; set; }
        public int TestType { get; set; }

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