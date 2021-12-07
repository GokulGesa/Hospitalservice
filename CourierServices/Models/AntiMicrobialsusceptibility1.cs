using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class AntiMicrobialsusceptibility1
    {
        public int AntiMicrobialId { get; set; }      
        public string MrdNo { get; set; }
        public string TestCode { get; set; }
        public string Agent { get; set; }
        public string zoneInhibition { get; set; }
        public string Interpretation { get; set; }
        public int CultureId { get; set; }
        public string MicrobialComment { get; set; }
        public string PatientID { get; set; }
    }
}


 