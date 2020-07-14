using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.ViewModels
{
    public class EditRoleViewModel
    {
        public string RoleId { get; set; }  
        [Required]
        [RegularExpression(@"^\S$|^\S[\s\S]*\S$", ErrorMessage = "Start and End Cannot Have Spaces")]
        public string RoleName { get; set;  }
    }
}
