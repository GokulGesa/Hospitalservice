using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class SampleCollect
    {
        public int SampleCollecterId { get;set;}
        public int TestID { get; set; }
        public int SampleStatus { get; set; }
        public string TestCode { get; set; }
        public string MrdNo { get; set; }
        public string TestName { get; set; }
        public string SampleName { get; set; }
        public int ProfileID { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public int ProfilePriority { get; set; }

        public string collected { get; set; }
        public string Notcollected { get; set; }
        public string Later { get; set; }


        public string SampleContainer { get; set; }

    }
}