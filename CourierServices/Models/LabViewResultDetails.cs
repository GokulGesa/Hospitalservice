using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class LabViewResultDetails
    {
        public int ID { get; set; }
        public string TestName { get; set; }
        public string TestCode { get; set; }
        public string Result { get; set; }
        public string NormalValue { get; set; }
        public string CriticalValue { get; set; }
        public string Units { get; set; }
        public string Comments { get; set; }
        public string SampleMrdNo { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public int ConfirmFlag { get; set; }
        public int SaveFlag { get; set; }
        public int CancelFlag { get; set; }
    }
}