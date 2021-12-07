using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class PrintReport
    {
        public string MrdNo { get; set; }
        public string RegID { get; set; }
        public string PatientName { get; set; }
        public string ReferredBy { get; set; }
        public string CollectAt { get; set; }
        public string ProviderName { get; set; }
        public string ProviderHostName { get; set; }
        public string CollectedDate { get; set; }
        public string ApprovedDate { get; set; }
        public string HosCreatedDate { get; set; }
        public string IsSamCollect { get; set; }
    }
}