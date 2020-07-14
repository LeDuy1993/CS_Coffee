using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CS_Coffee.ProductModels
{
    public class ProductCreateViewModel
    {
        [Required]
        [RegularExpression(@"^\S$|^\S[\s\S]*\S$", ErrorMessage = "Start and End Cannot Have Spaces")]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int ProductTypeId { get; set; }

        public IFormFile Avatar { get; set; }
    }
}
