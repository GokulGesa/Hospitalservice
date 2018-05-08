using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class HomeDetails
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public string PlayerContactDetails { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int status { get; set; }
        public int UserId { get; set; }
    }
}