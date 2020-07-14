using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;
using CS_Coffee.ProductModels;
using Microsoft.EntityFrameworkCore;

namespace CS_Coffee.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CoffeeContext context;

        public ProductRepository(CoffeeContext context)
        {
            this.context = context;
        }
        public Product Create(Product model)
        {
            var newPt = new Product()
            {
                ProductId = model.ProductId,
                Name = model.Name,
                AvatarPath = model.AvatarPath,
                Price = model.Price,
                ProductTypeId=model.ProductTypeId
            };
            context.Products.Add(newPt);
            context.SaveChanges();
            return model;
        }

        public bool Delete(int id)
        {
            var delProduct = context.Products.Find(id);
            if (delProduct != null)
            {
                context.Products.Remove(delProduct);
                return context.SaveChanges() > 0;
            }
            return false;
        }

        public Product Edit(Product product)
        {
            var editEmp = context.Products.Attach(product);
            editEmp.State = EntityState.Modified;
            context.SaveChanges();
            return product;
        }

        public Product Get(int id)
        {
            return context.Products.Find(id);
        }

        public IEnumerable<Product> Gets()
        {
            return context.Products;
        }
    }
}
