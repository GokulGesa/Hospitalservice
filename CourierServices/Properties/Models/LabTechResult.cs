using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class ResultLabTech
    {
        public int resultlabtestID { get; set; }
        public string MrdNo { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string TestID { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
        public string Comment2 { get; set; }
        public string CreateDate { get; set; }
        public string Units { get; set; }
        public string NormalValues { get; set; }
    }
}