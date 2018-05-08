using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class CollectedAt
    {
        public int CollectedId { get; set; }
        public int RegID { get; set; }
        public string CollectedName { get; set; }
        public string Date { get; set; }
        public int Amount { get; set; }
    }
}