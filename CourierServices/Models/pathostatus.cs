using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class pathostatus
    {
        public int ID { get; set; }
        public string MrdNo { get; set; }
        public string sampleid { get; set; }
        public string labstatus { get; set; }
        public string AcceptAll { get; set; }
        public string RejectAll { get; set; }
        public string AcceptSatus { get; set; }
        public string PatientID { get; set; }
    }
}