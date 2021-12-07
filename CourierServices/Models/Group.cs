using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string GroupOrginID { get; set; }
        public string GroupName { get; set; }
        public string NoOfPerson { get; set; }
        public string GroupCreatDate { get; set; }
        public double Amount { get; set; }
        public string HospitalName { get; set; }
        public string HospLocation { get; set; }
        public int HospitalID { get; set; }
        public string RefferedBy { get; set; }
        public string PaymentMode { get; set; }
        public string PaidAmount { get; set; }
        public string NetAmount { get; set; }

        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string SampleCollect { get; set; }
        public string ClientCode { get; set; }
    }
}