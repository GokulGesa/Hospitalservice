using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class PatientAllRegistrationInfo
    {
        public PatientRegistration patientRegistration { get; set; }
        public PatientEmergencyContact patientEmgContact { get; set; }
        public PatientSocialInfo patientSocialInfo { get; set; }
        public PatientContactInfo patientContactInfo { get; set; }

        //public int RegID { get; set; }
        //public string Title { get; set; }
        //public string FirstName { get; set; }
        //public string MiddleName { get; set; }
        //public string LastName { get; set; }
        //public string Guardian { get; set; }
        //public string Relation { get; set; }
        //public string PatientType { get; set; }
        //public string Sex { get; set; }
        //public string MaritalStatus { get; set; }
        //public string DOB { get; set; }
        //public int Age { get; set; }
        //public string DateOfReg { get; set; }
        //public long PhoneNumber { get; set; }
        //public byte[] ProfilePicture { get; set; }

        //public string ContactName { get; set; }
        //public string EmergencyRelationShip { get; set; }
        //public long ContactNo { get; set; }

        //public string SocialRelationship { get; set; }
        //public string Name { get; set; }
        //public string EmploymentStatus { get; set; }
        //public string EmployerName { get; set; }
        //public string EmployerAddress { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string IncomeGroup { get; set; }

        //public string DoorNo { get; set; }
        //public string FlatName { get; set; }
        //public string StreetName1 { get; set; }
        //public string StreetName2 { get; set; }
        //public string CCity { get; set; }
        //public string CState { get; set; }
        //public long MobileNO { get; set; }
        //public bool NotifySMS { get; set; }
        //public long PhoneNo { get; set; }
        //public string Locality { get; set; }
        //public long Pincode { get; set; }
        //public bool NotifyEmail { get; set; }
        //public string ContactEmail { get; set; }
    }
}