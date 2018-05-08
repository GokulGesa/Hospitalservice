using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class ProgressTable
    {
        public int Id { get; set; }
        public string ProgressName { get; set; }
        public string ProgressValue { get; set; }
        public string MrdNumber { get; set; }        
    }
}