using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Approver
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string CreateDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string MRD { get; set; }
        public string LapTest { get; set; }
        public string Status { get; set; }
    }
}