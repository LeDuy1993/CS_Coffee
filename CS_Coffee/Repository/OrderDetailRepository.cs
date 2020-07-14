 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CS_Coffee.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly CoffeeContext context;

        public OrderDetailRepository(CoffeeContext context)
        {
            this.context = context;
        }

        public OrderDetail Create(OrderDetail model)
        {
            var newOd = new OrderDetail()
            {
               OrderDetailId=model.OrderDetailId,
               OrderId=model.OrderId
            };
            context.OrderDetails.Add(newOd);
            context.SaveChanges();
            return model;
        }

        public bool Delete(int id)
        {
            var delOD = context.OrderDetails.Find(id);
            if (delOD != null)
            {
                context.OrderDetails.Remove(delOD);
                return context.SaveChanges() > 0;
            }
            return false;
        }

        public OrderDetail Edit(OrderDetail orderDetail)
        {
            var editOd = context.OrderDetails.Attach(orderDetail);
            editOd.State = EntityState.Modified;
            context.SaveChanges();
            return orderDetail;
        }
        public IEnumerable<OrderDetail> Edits(IEnumerable<OrderDetail> orderDetails)
        {
            foreach(var orderDetail in orderDetails)
            {
                var editOd = context.OrderDetails.Attach(orderDetail);
                editOd.State = EntityState.Modified;
                context.SaveChanges();
            }
            return orderDetails;
        }

        public OrderDetail Get(int id)
        { 
            return context.OrderDetails.Find(id);
        }
      
        public IEnumerable<OrderDetail> Gets()
        {
            return context.OrderDetails;
        }

     
    }
}
