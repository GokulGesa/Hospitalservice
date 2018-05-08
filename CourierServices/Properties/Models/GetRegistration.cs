using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class GetRegistration
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfReg { get; set; }
        public int RegId { get; set; }
        public string Sex { get; set; }
    }
}