using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class CancelledInvoice
    {
        public string InvoiceId { get; set; }
        public string comment { get; set; }
    }
}