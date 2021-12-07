using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class BioInfoLabView
    {

        public int IsPregnancy { get; set; }
        public string LMP { get; set; }
        public string Trimester { get; set; }
        public string RegID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }       
        public string DOB { get; set; }
        public string Age { get; set; }
        public string SpecialComments { get; set; }
        public string PatientID { get; set; }
        public string Title { get; set; }
        public string ExternalID { get; set; }
        public string PatEmail { get; set; }
        public string DocEmail { get; set; }
        public string ProviderName { get; set; }
    }
}