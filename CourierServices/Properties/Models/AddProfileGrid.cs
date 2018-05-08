using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class AddProfileGrid
    {
       
        public int ID { get; set; }
        public int RegID { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public double Amount { get; set; }
        public string MrdNo { get; set; }
        public string Discount { get; set; }
        public string NetAmount { get; set; }

    }
}