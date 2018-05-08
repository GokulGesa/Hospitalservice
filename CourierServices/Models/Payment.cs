using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int RegID { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public string InvoiceNo { get; set; }
        public string OrderNo { get; set; }
        public string MrdNo { get; set; }
        public double PaidAmount { get; set; }
        public double NetAmount { get; set; }
        public double PendingAmount { get; set; }
        public bool PaymentRecieved { get; set; }
        public string Status { get; set; }
        public string PaidBy { get; set; }
    }
}