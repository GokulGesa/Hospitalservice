using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class FreeTextTemplate
    {
        public int FreeTextID { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string TestType { get; set; }
        public string ElementName { get; set; }
        public int TestMultipleComponentsID { get; set; }
    }
}