using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.ViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string RolesName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        [RegularExpression("([0-9]{9,15})", ErrorMessage = "[0-9] 9-15 characters ")]
        public string PhoneNumber { get; set; }
        public bool LogoutEnable { get; set; }
       

    }
}
