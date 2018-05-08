using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class DriverUser
    {
        public int DId { get; set; }
        public string DriverID { get; set; }
        public string DriverFirstName { get; set; }
        public string DriverLastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int ProfileUpload { get; set; }
        public int IdentityUpload { get; set; }
        public int LicenseUpload { get; set; }
        public string IdentityProof { get; set; }
        public string IdentityNo { get; set; }
        public string DrivingLicenseNo { get; set; }
        public DateTime DOB { get; set; }
        public string CarType { get; set; }
        public int TerrifPackage { get; set; }
        public float Experience { get; set; }
        public string AccountNo { get; set; }
        public string BankBranch { get; set; }
        public string BankName { get; set; }
        public string IfscCode { get; set; }
        public int Verfication { get; set; }
        public string VerifiedBy { get; set; }
        public string DriverStatus { get; set; }
        public DateTime CreateDate { get; set; }
    }
}