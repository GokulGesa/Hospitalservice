using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class MultipleComponentswithCalculation
    {
        public int ComponentId { get; set; }
        public string MrdNo { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string ElementName { get; set; }
        public string Calculation { get; set; }
        public string Result { get; set; }
        public string Notes { get; set; }
        public string Comments { get; set; }
        public string ActualValue { get; set; }
        public string NormalValues { get; set; }
        public string Units { get; set; }
        public string DisplayValue { get; set; }
        public DateTime CreateDate { get; set; }
        public int RegId { get; set; }
        public int PriorityStatus { get; set; }
        public string Methodology { get; set; }
        public string FreeText { get; set; }
        public string NormalValuesFiled { get; set; }
        public int Status { get; set; }
        public bool select { get; set; }
        public string ProfileName { get; set; }

    }
}