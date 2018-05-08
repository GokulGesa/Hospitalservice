using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class GroupTestList
    {
        public int Sno { get; set; }
        public int GroupTestListId { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public double Amount { get; set; }
        public string MrdNo { get; set; }
        public string GroupName { get; set; }
    }
}