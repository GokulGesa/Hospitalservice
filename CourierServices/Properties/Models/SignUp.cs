using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourierServices.Models
{
    public class SignUp
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }      
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CurrentCity { get; set; }
        public int PinCode { get; set; }
        public string UserName { get; set; }       
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int Active { get; set; }
    }
}