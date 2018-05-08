using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class RateCard
    {
        public int Id { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string Amount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CreatorEmail { get; set; }
        public string CreatorPhoneNo { get; set; }
    }
}