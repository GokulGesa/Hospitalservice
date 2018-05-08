using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class GroupGenerateInvoice
    {
        public int Id { get; set; }
        public int RegID { get; set; }
        public string PatientName { get; set; }
        public string MrdNo { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
        public string Discount { get; set; }
        public string NetAmount { get; set; }
        public string PaidAmount { get; set; }
        public string Action { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Token { get; set; }

        public int Sno { get; set; }
        public int LabProfileListID { get; set; }
      //  public int RegID { get; set; }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
      //  public double Amount { get; set; }
      //  public string MrdNo { get; set; }
        public int ProfileID { get; set; }


    //    public int Sno { get; set; }
        public int LabTestListId { get; set; }
    //    public int RegID { get; set; }
        public string TestCode { get; set; }
        public string TestName { get; set; }
     //   public double Amount { get; set; }
     //   public string MrdNo { get; set; }

    }
}