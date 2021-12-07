using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class LabOrder
    {
        public int LabId { get; set; }
        public string RegID { get; set; }
        public string Age { get; set; }
        public string PatientName { get; set; }
        public string MrdNo { get; set; }
        public string ReferredBy { get; set; }
        public double ReferenceDiscountAmount { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string PaymentTypeName { get; set; }
        public string PaymentTypeCode { get; set; }
        public string InsuranceName { get; set; }
        public string CollectAt { get; set; }
        public double HomeCollectChargeAmount { get; set; }
        public int IsPregnancy { get; set; }
        public string LMP { get; set; }
        public string Trimester { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int ProfileID { get; set; }
        public string ProfileName { get; set; }
        public int TestID { get; set; }
        public string TestName { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public string Sex { get; set; }
        public string SpecialComments { get; set; }
        public string ProviderName { get; set; }
        public string ProviderID { get; set; }
        public string ProviderHostName { get; set; }
        public string LocationName { get; set; }
        public string LocationCode{ get; set; }

        public string AccountHolderName { get; set; }
        public string AccountHolderNumber { get; set; }

        public string ExternalId { get; set; }
    }
}
