using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.OrderModels
{
    public class OrderViewModel
    {
        [RegularExpression(@"^\S$|^\S[\s\S]*\S$", ErrorMessage = "Username Cannot Have Spaces")]
        public string Name { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderDetailId { get; set; }
        public int ProductOrderDetailId { get; set; }
        public string ReturnUrl { get; set; }
        public DateTime StartTime { get; set; }

        public int PodId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }

    }
}
