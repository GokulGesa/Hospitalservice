using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class CommonProfileTestDetails
    {
        public int LabProfileTestListID { get; set; }
        public int RegID { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string MrdNo { get; set; }
        public string PatientName { get; set; }
        public string Code { get; set; }
    }
}