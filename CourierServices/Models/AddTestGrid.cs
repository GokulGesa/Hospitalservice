using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class AddTestGrid
    {
        public int ID { get; set; }
        public int RegID { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public double Amount { get; set; }
        public string MrdNo { get; set; }
        public string Discount { get; set; }
        public string NetAmount { get; set; }
        public int Flag { get; set; }
        public string BiospyAbbrev { get; set; }
        public string BiospyFlag { get; set; }
    }
}