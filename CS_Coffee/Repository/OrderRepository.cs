using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;
using Microsoft.EntityFrameworkCore;

namespace CS_Coffee.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CoffeeContext context;

        public OrderRepository(CoffeeContext context)
        {
            this.context = context;
        }
        public IEnumerable<Order> Gets()
        {
            return context.Orders;
        }
        public Order Get(int id)
        {
            return context.Orders.Find(id);
        }

        public Order Edit(Order employee)
        {
            var editOd = context.Orders.Attach(employee);
            editOd.State = EntityState.Modified;
            context.SaveChanges();
          
            return employee;
           
        }
        
    }
}
