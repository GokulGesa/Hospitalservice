using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class PatientAllLabStatusInfo
    {
        //LaborderStatus Tabel
        public int LaborderStatusId { get; set; }
        public int RegID { get; set; }
        public string MrdNo { get; set; }
        public string LabStatus { get; set; }
        public string LabOrderDate { get; set; }
        public string ApproveStatus { get; set; }
        public string DenyStatus { get; set; }

        //LabOrder Tabel

        public int LabId { get; set; }
        //public int RegID { get; set; }
        public string PatientName { get; set; }
        //public string MrdNo { get; set; }

        //resultlabtest Tabel
        public int ResultLabId { get; set; }
        public string MrdNum { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public string DepartmentID { get; set; }

        public string Methodology { get; set; }
        public string SampleContainer { get; set; }
        public string DepartmentName { get; set; }
        public string UnitMeasurementNumeric { get; set; }

        public string UnitMesurementFreeText { get; set; }
        public string TableRequiredPrint { get; set; }
        public string multiplecomponents { get; set; }
        public string DefaultValues { get; set; }


        public string GenderMale { get; set; }
        public string GenderFemale { get; set; }
        public string Pregnancyrefrange { get; set; }
        public string AdditionalFixedComments { get; set; }

        public string LowerCriticalValue { get; set; }
        public string UpperCriticalValue { get; set; }
        public string OtherCriticalReport { get; set; }
        public string AgewiseCriticalValue { get; set; }

        public string AgewiseReferenceRange { get; set; }
        public string units { get; set; }
        public string TurnAroundTime { get; set; }
        public string RequiredBiospyTestNumber { get; set; }

        public string RequiredSamples { get; set; }
        public string PatientPreparation { get; set; }
        public string ExpectedResultDate { get; set; }
        public string Finaloutput { get; set; }

        public string Outsourced { get; set; }
        public string CreateDate { get; set; }
        public string Result { get; set; }
        public string Comment1 { get; set; }
        public string Comment2 { get; set; }

        public int count { get; set; }

    }
}