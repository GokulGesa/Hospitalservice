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
        public string ProfileID { get; set; }
        public string ProfileName { get; set; }
        public string MrdNo { get; set; }
        public string TestName{ get; set; }
        public string SampleContainer { get; set; }
        public int SampleStatus { get; set; }
        public string units { get; set; }
        public string AlternativeSampleName { get; set; }
    }
}