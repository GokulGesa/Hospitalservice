using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class AgewiseCricticalReferences
    {
        public int AgewiseCricticalValueID { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string StartDayCrictical { get; set; }
        public string EndDayCrictical { get; set; }
        public string StartMonthCrictical { get; set; }
        public string EndMonthCrictical { get; set; }
        public string StartYearCrictical { get; set; }
        public string EndYearCrictical { get; set; }
        public string LowUpperCricticalValue { get; set; }
    }
}