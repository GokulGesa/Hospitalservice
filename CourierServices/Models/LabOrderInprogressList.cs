using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class LabOrderInprogressList
    {
        public string FirstName { get; set; }
        public string PatientName { get; set; }
        public string MrdNo { get; set; }
        public string LabStatus { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string RegID { get; set; }
        public string sex { get; set; }
        public string Age { get; set; }
        public string CreateDate { get; set; }
        public string LocationName { get; set; }
        public string PatientID { get; set; }
        public string TotalSamples { get; set; }
        public string TotalEntered { get; set; }
        public string TotalConfirmed { get; set; }
        public string TotalApproved { get; set; }
        public string PendingApproval { get; set; }
        public string FinalStatus { get; set; }
        public int DayCount { get; set; }
    }
}