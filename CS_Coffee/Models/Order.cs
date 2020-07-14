using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.Models
{
    public class Order
    {
        
        public int OrderId { get; set; }
        public bool Using { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
    
    }
}
