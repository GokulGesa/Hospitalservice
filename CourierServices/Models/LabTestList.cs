using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class LabTestList
    {
        
            public int Sno { get; set; }
            public int LabTestListId { get; set; }
            public int RegID { get; set; }
            public string TestCode { get; set; }
            public string TestName { get; set; }
            public double Amount { get; set; }
            public string MrdNo { get; set; }
            public string PatientName { get; set; }
            public int IndividualStatus { get; set; }
        

    }
}