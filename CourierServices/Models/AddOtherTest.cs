using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class AddOtherTest
    {
        public int OthTestId { get; set; }
        public int RegID { get; set; }
        public int ProfileID { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public double Amount { get; set; }
        public string MrdNo { get; set; }
        public string Discount { get; set; }
        public string NetAmount { get; set; }      
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public int InsertFlag { get; set; }

    }
}