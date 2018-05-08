using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class TestMultipleComponents
    {
        public int TestMultipleComponentsID { get; set; }
        public string TestCode { get; set; }
        public string ElementName { get; set; }
        public string Color { get; set; }
        public string Units { get; set; }
        public string Comments { get; set; }
        public string CriticalLow { get; set; }
        public string CriticalHigh { get; set; }
        public string ReferenceLow { get; set; }
        public string ReferenceHigh { get; set; }
    }
}