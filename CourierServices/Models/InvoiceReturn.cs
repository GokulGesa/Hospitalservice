using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class InvoiceReturn
    {
        public int Sno { get; set; }
        public int InvoiceReturnId { get; set; }
        public int RegID { get; set; }
        public string InvoiceNo { get; set; }
        public string ReturnAmount { get; set; }
        public string ReturnDate { get; set; }
        public string ReturnReason { get; set; }
        public string ReturnStatus { get; set; }

    }

}