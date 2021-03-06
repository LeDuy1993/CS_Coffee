﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Confirm Password not match")]
        public string ConfirmPassword { get; set; }
        [RegularExpression(@"^\S$|^\S[\s\S]*\S$", ErrorMessage = "Start and End Cannot Have Spaces")]
        public string City { get; set; }
        [RegularExpression(@"^\S$|^\S[\s\S]*\S$", ErrorMessage = "Start and End Have Spaces")]
        public string Address { get; set; }
        [RegularExpression("([0-9]{9,15})", ErrorMessage = "Not a phone number")]
        public string PhoneNumber { get; set; }
    }
}
