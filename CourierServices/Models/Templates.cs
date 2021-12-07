using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Templates
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateValue { get; set; }
        public int ElementId { get; set; }
        public string ElementName { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public int Flag { get; set; }
    }
}