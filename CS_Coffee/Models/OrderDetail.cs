using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public bool Paid { get; set; }
        public string UserId { get; set; }
        public ICollection<ProductOrderDetail> ProductOrderDetails { get; set; }

    }
}
