using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class PragancyReferancyRange
    {
        public int PregnancyId { get; set; }
        public string TestCode { get; set; }
        public string Parameter { get; set; }
        public int RegID { get; set; }        
        public string FirstTrimester { get; set; }
        public string SecondTrimester { get; set; }
        public string ThirdTrimester { get; set; }
        public string CreateDate { get; set; }
        public int Flag { get; set; }
             
    }
}