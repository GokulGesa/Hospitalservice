using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class PatientSocialInfo
    {
        public int RegID { get; set; }
        public string SocialRelationship { get; set; }
        public string Name { get; set; }
        public string EmploymentStatus { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string IncomeGroup { get; set; }
    }
}

