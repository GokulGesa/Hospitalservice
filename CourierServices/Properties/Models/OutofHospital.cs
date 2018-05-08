using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class OutofHospital
    {
        public int OutofHospitalId { get; set; }
        public int RegID { get; set; }
        public string ProfileName { get; set; }
        public string o_Ho_Name { get; set; }
        public double Amount { get; set; }
    }
}