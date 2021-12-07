using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class InsuranceAdminLevel
    {
        public int InsuranceID { get; set; }
        public string InsuranceOrginID { get; set; }
        public string InsuranceName { get; set; }
        public string InsuranceCreatDate { get; set; }
        public double Amount { get; set; }     
        public string Location { get; set; } 
    }
}