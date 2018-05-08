using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class MasterProfileList
    {
        public int Sno { get; set; }
        public int ProfileID { get; set; }
        public int DepartmentID { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public double Amount { get; set; }
        public string CreateDate { get; set; }
    }
}