using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class LabProfileList
    {
        public int Sno { get; set; }
        public int LabProfileListID { get; set; }
        public int RegID { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public double Amount { get; set; }
        public string MrdNo { get; set; }
        public int ProfileID { get; set; }
        public string PatientName { get; set; }
        public string SampleName { get; set; }
        public int IndividualStatus { get; set; }
        public int SampleStatus { get; set; }
    }
}