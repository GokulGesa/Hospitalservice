using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class BarCode
    {
        public int BarCodeID { get; set; }
        public int RegID { get; set; }
        public string PatientName { get; set; }
        public string MrdNo { get; set; }
        public string BarCodeKey { get; set; }
    }
}