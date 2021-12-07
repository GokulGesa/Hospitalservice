using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string status { get; set; }
        public string Flag { get; set; }
    }
}