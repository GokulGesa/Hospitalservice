using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Agewisereferencevalue
    {
        public int AgewiseReferenceValueID { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
      
        public string StartDay { get; set; }
        public string EndDay { get; set; }
        public string StartMonth { get; set; }
        public string EndMonth { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public string Age { get; set; }
        public string Dob { get; set; }

        public string LowUpperReferenceValue { get; set; }

    }
}