using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class HospitalLinkDoctors
    {
        public int CodeId { get; set; }
        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
        public string HospitalLinkDoctName { get; set; }
        public string ClientCode { get; set; }
        public string GetDate { get; set; }
        public int CodeStatus { get; set; }
        public int CodeFlag { get; set; }
        public string IsMailRequired { get; set; }
        public string EmailID { get; set; }
        public string PhoneNO { get; set; }
        public string Location { get; set; }
        public string PanNo { get; set; }
        public string Address { get; set; }
    }
}