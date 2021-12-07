using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class AntiMicrobialsusceptibilityNew
    {
        public int AntiMicrobialId { get; set; }       
        public string Agent { get; set; }
        public string zoneInhibition { get; set; }
        public string Interpretation { get; set; }
        public int CultureId { get; set; }

        public int CultureTestResultId { get; set; }
        public string MrdNo { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string Specimen { get; set; }
        public string GrainStain { get; set; }
        public string SpecialStain { get; set; }
        public string Result { get; set; }
        public string OrganizismIsolated { get; set; }
        public string ColonyCount { get; set; }
        public string Comments { get; set; }
        public string OrganizismIsolated2 { get; set; }
        public string ColonyCount2 { get; set; }
        public string Comments2 { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }

        public int AgentId { get; set; }
        public string Category { get; set; }
        public string PrelimContent { get; set; }
        public string NegComment { get; set; }
        public int EditFlag { get; set; }
        public int EditMicrobID { get; set; }
        public string CulComments { get; set; }
        public string QualityScore { get; set; }
        public string FastBacilli { get; set; }
        public string MicroBComments { get; set; }

        public string Outsourced { get; set; }
        public string Sex { get; set; }
    }
}