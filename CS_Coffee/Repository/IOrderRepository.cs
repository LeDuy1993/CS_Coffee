using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;
using Microsoft.AspNetCore.Identity;

namespace CS_Coffee.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Gets();
        Order Get(int id);
        Order Edit(Order employee);

    }
}
