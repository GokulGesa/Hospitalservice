using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public string GroupOrginID { get; set; }
        public string GroupName { get; set; }
        public string NoOfPerson { get; set; }
        public string GroupCreatDate { get; set; }
        public double Amount { get; set; }
    }
}