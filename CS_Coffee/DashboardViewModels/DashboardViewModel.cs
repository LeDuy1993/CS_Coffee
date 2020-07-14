using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS_Coffee.DashboardViewModels
{
    public class DashboardViewModel
    {
        public int OrderID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }

        public int Price { get; set; }
        public int Total { get; set; }
        public string ProductName { get; set; }

    }
}
