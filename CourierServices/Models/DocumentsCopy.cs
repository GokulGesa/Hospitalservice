using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class DocumentsCopy
    {
        public int DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string DriverId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }       
        public DateTime CreateDate { get; set; }
    }
}