using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class subprofile
    {
        public int ID { get; set; }
        public string MainProfileCode { get; set; }
        public string SubProfileCode { get; set; }
        public string SubProfileId { get; set; }
        public string MrdNo { get; set; }
        public string TestCode { get; set; }
        public string BioAbbrev { get; set; }
        public string BiospyNo { get; set; }
        public string PatientName { get; set; }
        public string GroupName { get; set; }
        public int GroupCount { get; set; }

    }
}