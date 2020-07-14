using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.Models
{
    public class ProductType
    {
        [Required]
        public int ProductTypeId { get; set; }
        public string TypeName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
