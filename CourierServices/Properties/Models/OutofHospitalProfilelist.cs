using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class OutofHospitalProfilelist
    {
        public int OutOfHospitalProfileListID { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public double Amount { get; set; }
        public string MrdNoOutOfHospital { get; set; }
        public string HospitalName { get; set; }
    }
}