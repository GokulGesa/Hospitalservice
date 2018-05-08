using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class PatientEmergencyContact
    {
        public int RegID { get; set; }
        public string ContactName { get; set; }
        public string EmergencyRelationShip { get; set; }
        public long ContactNo { get; set; }
    }
}
