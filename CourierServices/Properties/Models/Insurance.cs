using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Insurance
    {
        public int InsuranceId { get; set; }
        public int RegID { get; set; }
        public string InsuranceProviderName { get; set; }
        public string ContactNo { get; set; }
        public double Amount { get; set; }
    }
}