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
        public int RegID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }       
        public string DOB { get; set; }
        public int Age { get; set; }
        public string SpecialComments { get; set; }
    }
}