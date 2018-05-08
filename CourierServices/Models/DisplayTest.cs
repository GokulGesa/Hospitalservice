using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class DisplayTest
    {
        public int DisplayTestID { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string DisplayName { get; set; }
        public double Amount { get; set; }
        public string ExpiryDate { get; set; }
    }
}