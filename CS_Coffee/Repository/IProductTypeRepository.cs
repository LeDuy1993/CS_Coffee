using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;

namespace CS_Coffee.Repository
{
   public interface IProductTypeRepository
    {
        IEnumerable<ProductType> Gets();
        ProductType Get(int id);
    }
}
