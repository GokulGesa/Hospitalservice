using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class LabOrderTestList
    {
        public int TestID { get; set; }
        public int ProfileID { get; set; }
        public string TestName { get; set; }
        public Double Amount { get; set; }
    }
}