using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class AntiMicrobialsusceptibility2
    {
        public int AntiMicrobialId { get; set; }
        public string MrdNo { get; set; }
        public string TestCode { get; set; }
        public string Agent { get; set; }
        public string zoneInhibition { get; set; }
        public string Interpretation { get; set; }
    }
}