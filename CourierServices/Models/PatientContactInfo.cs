using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class PatientContactInfo
    {
        public int RegID { get; set; }
        public string DoorNo { get; set; }
        public string FlatName { get; set; }
        public string StreetName1 { get; set; }
        public string StreetName2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long MobileNO { get; set; }
        public bool NotifySMS { get; set; }
        public long PhoneNo { get; set; }
        public string Locality { get; set; }
        public long Pincode { get; set; }
        public bool NotifyEmail { get; set; }
        public string ContactEmail { get; set; }
        public string Country { get; set; }
    }
}

 