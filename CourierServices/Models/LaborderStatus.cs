using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class LaborderStatus
    {
        public int LaborderStatusId { get; set; }
        public string RegID { get; set; }
        public string MrdNo { get; set; }
        public string LabStatus { get; set; }
        public DateTime LabOrderDate { get; set; }
        public string ApproveStatus { get; set; }
        public string DenyStatus { get; set; }
        public int  SampleStatus { get; set; }
        public int CancelFlag { get; set; }
        public string CancelComment { get; set; }

    }
}