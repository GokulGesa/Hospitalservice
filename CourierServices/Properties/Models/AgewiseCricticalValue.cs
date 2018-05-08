using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class AgewiseCricticalValue
    {
        public int AgewiseCricticalValueID { get; set; }
        public string TestCode { get; set; }
        public string Parameter { get; set; }
        public string Birth { get; set; }
        public string Days3 { get; set; }
        public string Week1 { get; set; }
        public string Weeks2 { get; set; }
        public string Weeks3 { get; set; }
        public string Month1 { get; set; }
        public string Months2 { get; set; }
        public string Months6 { get; set; }
        public string year1 { get; set; }
        public string years6 { get; set; }
        public string years12 { get; set; }
        public string Men { get; set; }
        public string Women { get; set; }

    }
}