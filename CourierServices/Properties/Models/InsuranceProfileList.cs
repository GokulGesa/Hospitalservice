using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class InsuranceProfileList
    {
        public int InsuranceProfileListID { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public double Amount { get; set; }
        public string MrdNoInsurance { get; set; }
        public string InsuranceName { get; set; }
        
    }
}