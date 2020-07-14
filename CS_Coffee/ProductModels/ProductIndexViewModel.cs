using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.ProductModels
{
    public class ProductIndexViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string AvatarPath { get; set; }
        public int ProductTypeId { get; set; }
        public string TypeName { get; set; }

    }
}
