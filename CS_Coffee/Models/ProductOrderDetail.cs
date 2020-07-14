using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.Models
{
    public class ProductOrderDetail
    {
       
        public int ProductOrderDetailId { get; set; } 
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderDetailId { get; set; }
        public OrderDetail OrderDetail { get; set; }
        [Range(1,100)]
        public int Count { get; set; }

    }
}
