using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.ViewModels;

namespace CS_Coffee.UserViewModels
{
    public class UserEditViewModel 
    {
        public string UserId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^\S$|^\S[\s\S]*\S$", ErrorMessage = "Start and End Cannot Have Spaces")]
        public string City { get; set; }
        [RegularExpression(@"^\S$|^\S[\s\S]*\S$", ErrorMessage = "Start and End Cannot Have Spaces")]
        public string Address { get; set; }
        public string RoleId { get; set; }
        [RegularExpression("([0-9]{9,15})", ErrorMessage = "[0-9] 9-15 characters ")]
        public string PhoneNumber { get; set; }

    }
}
