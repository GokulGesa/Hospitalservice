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
    }
}