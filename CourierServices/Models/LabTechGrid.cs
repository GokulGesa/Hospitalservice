using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class LabTechGrid
    {
        

            public int TestID { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string Units { get; set; }
        public string SampleName { get; set; }
        public int SampleStatus { get; set; }
        public int IndividualStatus { get; set; }
    }
}