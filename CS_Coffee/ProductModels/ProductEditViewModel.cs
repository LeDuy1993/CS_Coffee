using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.ProductModels
{
    public class ProductEditViewModel : ProductCreateViewModel
    {
        public int ProductId { get; set; }
        public string AvatarPath { get; set; }
    }
}
