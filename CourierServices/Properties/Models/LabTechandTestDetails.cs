using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class LabTechandTestDetails
    {
        public ChildTestList ChildTestListDetails { get; set; }
        public LabOrder LabOrderDetails { get; set; }
    }
}