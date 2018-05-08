using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class OutofHospitalTestlist
    {
        public int OutOfHospitalTestListId { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public double Amount { get; set; }
        public string MrdNoOutOfHospital { get; set; }
        public string HospitalName { get; set; }
    }
}