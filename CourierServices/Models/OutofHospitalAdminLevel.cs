using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class OutofHospitalAdminLevel
    {
        public int OutOfHospitalID { get; set; }
        public string HospitalOrginID { get; set; }
        public string HospitalName { get; set; }
        public string OutOfHospitalCreatDate { get; set; }
        public double Amount { get; set; }
        public string LocationName { get; set; }
        public string LocationCode { get; set; }
    }
}