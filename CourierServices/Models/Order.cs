using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int RegID { get; set; }
        public int LabId { get; set; }
        public string OrderDate { get; set; }
    }
}