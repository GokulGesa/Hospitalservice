using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{

    public class BoneMarrowAspirationNew
    {
        public int bonemarrowaspirationid { get; set; }
        public string MrdNo { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public int ElementId { get; set; }
        public string ElementName { get; set; }
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDescription { get; set; }
        public int Status { get; set; }
        public bool select { get; set; }
        public string PatientID { get; set; }
        public string CreatedDateTime { get; set; }
        public string ByspyNo { get; set; }
        public string Outsourced { get; set; }
        public string Sex { get; set; }

        public int MarkerID { get; set; }
        public string MarkerName { get; set; }
        public int MarkerPriority { get; set; }

        public int ImmunoMarkerID { get; set; }
        public string Clone { get; set; }
        public string Results { get; set; }
        public string Impressions { get; set; }
        public string PriorityStatus { get; set; }

    }
}