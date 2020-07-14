using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;
using Microsoft.EntityFrameworkCore;

namespace CS_Coffee.Repository
{
    public class ProductOrderDetailRepository : IProductOrderDetailRepository
    {
        private readonly CoffeeContext context;

        public ProductOrderDetailRepository(CoffeeContext context)
        {
            this.context = context;
        }

        public ProductOrderDetail Create(ProductOrderDetail model)
        {
            
            var newPod = new ProductOrderDetail()
            {
                ProductId = model.ProductId,
                OrderDetailId = model.OrderDetailId,
                Count = model.Count,
                ProductOrderDetailId = model.ProductOrderDetailId
            };
            context.ProductOrderDetails.Add(newPod);
            context.SaveChanges();
            return model;
        }

        public bool Delete(int id)
        {
            var delPOD = (from pod in context.ProductOrderDetails.ToList()
                          where pod.ProductOrderDetailId == id
                          select pod).FirstOrDefault();
            if (delPOD != null)
            {
                context.ProductOrderDetails.Remove(delPOD);
                return context.SaveChanges() > 0;
            }
            return false;
        }

        public ProductOrderDetail Edit(ProductOrderDetail productOrderDetail)
        {
            var editPOD = context.ProductOrderDetails.Attach(productOrderDetail);
            editPOD.State = EntityState.Modified;
            context.SaveChanges();
            return productOrderDetail;
        }
      

        public ProductOrderDetail Get(int id)
        {

            return (from pod in context.ProductOrderDetails.ToList()
                    where pod.ProductOrderDetailId == id
                    select pod).FirstOrDefault();
        }

        public IEnumerable<ProductOrderDetail> Gets()
        {
            return context.ProductOrderDetails;
        }

    }
}
