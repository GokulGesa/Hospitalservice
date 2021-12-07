using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class InvoiceView
    {
        public int Id { get; set; }
        public string RegID { get; set; }
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
        public string ReferredBy { get; set; }
        public string ProviderID { get; set; }
        public int CollectionCharge { get; set; }
        public int SampleCount { get; set; }
        public int TotSampleCount { get; set; }
        public int LabProfileTestListID { get; set; }
        public string Code { get; set; }
        public string TotalAmount { get; set; }
        public int GrossTotal { get; set; }
        public double NetTotal { get; set; }
        public double GrossDisCount { get; set; }
        public string INVNumber { get; set; }
        public string INVDateFrom { get; set; }
        public string INVDateTo { get; set; }
        public string ClientCode { get; set; }
        public string CreatedDate { get; set; }
        public string INVType { get; set; }
        public double TotalGrossAmount { get; set; }
        public double TotalNetAmnt { get; set; }
        public string GrossPayType { get; set; }
        public int totsampForRange { get; set; }
        public int chksampForRange { get; set; }
        public string InvoiceStatus { get; set; }

    }
}