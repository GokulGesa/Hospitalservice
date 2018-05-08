using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class PersonalInfo
    {

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CurrentCity { get; set; }
        public int PinCode { get; set; }
        public string batRole { get; set; }
        public string righthandBat { get; set; }
        public string lefthandBat { get; set; }
        public string righthandBowl { get; set; }
        public string lefthandBowl { get; set; }
        public string BowlerRole { get; set; }
        public string Role { get; set; }
       

    }
}