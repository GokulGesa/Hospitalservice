using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class LabTestandProfileDetails
    {

        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public string MrdNo { get; set; }
        public string SampleContainer { get; set; }
        public string AlternativeSampleContainer { get; set; }
        public string TableRequired { get; set; }

    }
}