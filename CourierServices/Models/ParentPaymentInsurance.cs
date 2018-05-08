using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class ParentPaymentInsurance
    {

        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CreditInvoice { get; set; }
        public string InsuranceName { get; set; }
        public double Amount { get; set; }
        public double ReceivedPayment { get; set; }
        public double PendingAmount { get; set; }
        public string Status { get; set; }
        public string PendingNotification { get; set; }
        public string strPaymentSchedule { get; set; }
        public DateTime datePaymentSchedule { get; set; }
    }
}