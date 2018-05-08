using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class GroupProfileList
    {
        public int Sno { get; set; }
        public int GroupProfileListID { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public double Amount { get; set; }
        public string MrdNo { get; set; }
        public string GroupName { get; set; }
    }
}