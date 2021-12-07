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

        //added for signature upload
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Age { get; set; }
        public string UGDegree { get; set; }
        public string UgUniver { get; set; }
        public string PGDegree { get; set; }
        public string PGUniver { get; set; }

        public string College { get; set; }
        public string OtherDegree { get; set; }
        public string RegistNumber { get; set; }
        public string Designation { get; set; }
        public string DesigForReport { get; set; }
        public string ImageName { get; set; }
        public string PicturePath { get; set; }
        public string CreateDate { get; set; }
        public int Priority { get; set; }
        public bool Checked { get; set;}
        public string DocDesigForRpt { get; set; }
        public string MailRequired { get; set; }
        public int Flag { get; set; }
    }
}