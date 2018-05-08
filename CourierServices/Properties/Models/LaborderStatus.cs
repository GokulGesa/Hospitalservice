using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class LaborderStatus
    {
        public int LaborderStatusId { get; set; }
        public int RegID { get; set; }
        public string MrdNo { get; set; }
        public string LabStatus { get; set; }
        public string LabOrderDate { get; set; }
        public string ApproveStatus { get; set; }
        public string DenyStatus { get; set; }
        public int  SampleStatus { get; set; }
    }
}