using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.Models
{
    public class Product
    {
        public int ProductId { get; set; }  
        [Required]
        public string Name { get; set; }
    
        [Required]
        [Range(1000, int.MaxValue)]
        public int Price { get; set; }
        public string AvatarPath { get; set; }
        public ICollection<ProductOrderDetail> ProductOrderDetails { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

    }
}
