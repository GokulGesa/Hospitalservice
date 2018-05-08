using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Registration
    {
        public int MRD { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatientType { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public string DOB { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CreateDate { get; set; }
    }
}