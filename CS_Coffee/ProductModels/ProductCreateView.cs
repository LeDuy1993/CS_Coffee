using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;

namespace CS_Coffee.ProductModels
{
    public class ProductCreateView
    {
        public int ProductId { get; set; }
        [Required]
        [RegularExpression(@"^\S$|^\S[\s\S]*\S$", ErrorMessage = "Start and End Cannot Have Spaces")]
        public string Name { get; set; }
        [Required]
        [Range(1000, int.MaxValue)]
        public int Price { get; set; }
        public string AvatarPath { get; set; }
        public int ProductTypeId { get; set; }

    }
}
