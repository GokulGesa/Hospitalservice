using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Invoice
    {
        
        public int Id { get; set; }
        public int RegID { get; set; }
        public string PatientName { get; set; }
        public string MrdNo { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
        public string Discount { get; set; }
        public string NetAmount { get; set; }
        public string PaidAmount { get; set; }
        public string Action { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }
        public string ProviderHostName { get; set; }
        public string HospitalName { get; set; }
    }
}