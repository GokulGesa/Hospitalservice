using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class calculationForTestDetails
    {
        public int id { get; set; }
        public string testCode { get; set; }
        public string calculation { get; set; }
        public string splcalculation { get; set; }
        public string CalculationUnits { get; set; }
        public string FormulaLabel { get; set; }
        public string CalculationType { get; set; }
        public string TestCodesCalculationPart { get; set; }
        public string ElementName { get; set; }
        public string CalculationCategory { get; set; }
        public string NormalValues { get; set; }
        public string ElementsCalculationType { get; set; }
        public int Flag { get; set; }

    }
}