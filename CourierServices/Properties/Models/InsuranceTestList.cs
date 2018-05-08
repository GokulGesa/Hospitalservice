using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class InsuranceTestList
    {
        public int InsuranceTestListId { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public double Amount { get; set; }
        public string MrdNoInsurance { get; set; }
        public string InsuranceName { get; set; }
    }
}