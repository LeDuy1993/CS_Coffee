using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;

namespace CS_Coffee.OrderModels
{
    public class OrderCreateViewModel
    {
        public int ProductOrderDetailId { get; set; }
        public int TableId { get; set; }
        public bool Using { get; set; }
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        [RegularExpression(@"^\S$|^\S[\s\S]*\S$", ErrorMessage = "Username Cannot Have Spaces")]
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
