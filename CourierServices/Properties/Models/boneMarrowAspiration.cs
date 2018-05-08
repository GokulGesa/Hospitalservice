using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class BoneMarrowAspiration
    {
        public int Id { get; set; }
        public string id_MrdNumber { get; set; }
        public string SampleId_mrdnumber { get; set; }
        public string ClinicalFindings { get; set; }
        public string PeripheralBloodFindings { get; set; }
        public string BoneMarrowNumber { get; set; }
        public string Cellularity { get; set; }
        public string Erythropoiesis { get; set; }
        public string Myelopoiesis { get; set; }
        public string M_E { get; set; }
        public string Eosinophils { get; set; }
        public string Lymphocytes { get; set; }
        public string PlasmaCells { get; set; }        
        public string Blasts { get; set; }
        public string Megakaryocytes { get; set; }
        public string Others { get; set; }
        public string Perl_sIronStain { get; set; }
        public string Impression { get; set; }
        public string Advice { get; set; }
        public string Status { get; set; }
    }
}