using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class GroupRegistration
    {
        public int MRD { get; set; }
        public int RegID { get; set; }
        public string GroupName { get; set; }
        public int NoOfPerson { get; set; }
        public string Amount { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Guardian { get; set; }
        public string Relation { get; set; }
        public string PatientType { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public string DOB { get; set; }
        public int Age { get; set; }
        public string DateOfReg { get; set; }
        public long PhoneNumber { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}