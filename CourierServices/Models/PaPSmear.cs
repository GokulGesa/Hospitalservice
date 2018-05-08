using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class PaPSmear
    {
        public int id { get; set; }

        public string id_MrdNumber { get; set; }
        public string SampleId_mrdnumber { get; set; }
        public string Cytologynumber { get; set; }
        public string Numberofsildes { get; set; }
             
        public string Reportingsystem { get; set; }
        public string Specimentype { get; set; }
        public string SpecimenAdequacy { get; set; }
        public string NonNeoplasticFindings { get; set; }
               
        public string withoutCol1 { get; set; }
        public string withoutCol2 { get; set; }
        public string Organisms { get; set; }
        public string withoutCol3 { get; set; }
               
        public string Others { get; set; }
        public string EpithelialcellAbnormalities { get; set; }
        public string withoutCol4 { get; set; }
        public string Interpretation { get; set; }
               
        public string Educationalnotesandcomments { get; set; }
        public string Status { get; set; }        
    }
}