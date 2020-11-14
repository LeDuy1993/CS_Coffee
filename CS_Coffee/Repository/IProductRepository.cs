using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;
using CS_Coffee.ProductModels;

namespace CS_Coffee.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> Gets(string search = null);
        Product Get(int id);
        Product Create(Product model);
        Product Edit(Product employee);
        bool Delete(int id);
    }
}
