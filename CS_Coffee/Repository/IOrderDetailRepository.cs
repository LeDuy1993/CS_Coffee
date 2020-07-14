using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;

namespace CS_Coffee.Repository
{
   public interface IOrderDetailRepository
    {
        OrderDetail Create(OrderDetail model);
        IEnumerable<OrderDetail> Gets();
        OrderDetail Get(int id);
        bool Delete(int id);
        OrderDetail Edit(OrderDetail model);
        IEnumerable<OrderDetail> Edits(IEnumerable<OrderDetail> orderDetails);
    }
}
