using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Addprofile
    {

        public int ProfileID { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public string SampleName { get; set; }
        public double Amount { get; set; }
        public string CreateDate { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public double Priority { get; set; }
        public double TestPriority { get; set; }
        public string SubProfileCode { get; set; }
        public string SubProfileName { get; set; }
        public string flag { get; set; }
        public double ProfilePriority { get; set; }
        public string MainProfileCode { get; set; }
        public double SubProfilePriority { get; set; }
        public string DisplayName { get; set; }

    }
}