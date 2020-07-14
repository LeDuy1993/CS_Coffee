using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;

namespace CS_Coffee.Repository
{
    public interface IProductOrderDetailRepository
    {
        ProductOrderDetail Create(ProductOrderDetail model);
        IEnumerable<ProductOrderDetail> Gets();
        ProductOrderDetail Get(int id);
        ProductOrderDetail Edit(ProductOrderDetail productOrderDetail);
        bool Delete(int id);
    }
}
