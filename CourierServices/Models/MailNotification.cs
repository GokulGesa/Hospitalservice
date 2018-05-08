using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class MailNotification
    {

         public int MailId { get; set; }
        public string FromMail { get; set; }
        public string ToMail { get; set; }
        public string CCMail { get; set; }
        public string BCCMail { get; set; }
        public string MailSubject { get; set; }
        public string BodyMail { get; set; }
        public string Attachment { get; set; }
    }
}