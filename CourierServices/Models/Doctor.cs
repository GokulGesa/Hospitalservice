using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public int RegID { get; set; }
        public string DoctorName { get; set; }
        public string EmailId { get; set; }
        public int PhoneNo { get; set; }
    }
}