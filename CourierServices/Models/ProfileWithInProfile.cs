using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class ProfileWithinProfile
    {
        public int ID { get; set; }
        public string MainProfileId { get; set; }
        public string MainProfileCode { get; set; }
        public int SubProfileId { get; set; }
        public string SubProfileName { get; set; }
        public string SubProfileCode { get; set; }
        public string flag { get; set; }
        public double ProfilePriority { get; set; }
    }
}