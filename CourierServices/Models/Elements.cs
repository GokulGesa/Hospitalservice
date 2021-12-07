using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Elements
    {
        public int ElementId { get; set; }
        public string ElementName { get; set; }
        public string ElementValue { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string CreatedDate { get; set; }
        public int PriorityStatus { get; set; }
    }
}