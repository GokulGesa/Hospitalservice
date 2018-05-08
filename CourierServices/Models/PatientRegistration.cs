using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class PatientRegistration
    {
        public int RegID { get; set; }
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
        public string PhoneNumber { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string SpecialComments { get; set; }
        public string ContactEmail { get; set; }

        public int AgeDay { get; set; }
        public int AgeMonth { get; set; }
        public int AgeYear { get; set; }

        public int UnknownAge { get; set; }
        public string AgeCategory { get; set; }
    }
}



      