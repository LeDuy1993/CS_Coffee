using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;

namespace CS_Coffee.Repository
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly CoffeeContext context;

        public ProductTypeRepository(CoffeeContext context)
        {
            this.context = context;
        }

        public ProductType Get(int id)
        {
            return context.ProductTypes.Find(id);
        }

        public IEnumerable<ProductType> Gets()
        {
            return context.ProductTypes;
        }
    }
}
