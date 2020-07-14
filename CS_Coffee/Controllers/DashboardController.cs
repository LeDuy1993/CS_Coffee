using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.DashboardModels;
using CS_Coffee.DashboardViewModels;
using CS_Coffee.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CS_Coffee.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductOrderDetailRepository productOrderDetailRepository;
        private readonly IProductRepository productRepository;
        private readonly IProductTypeRepository productTypeRepository;

        public DashboardController(ILogger<DashboardController> logger,
                              IOrderDetailRepository orderDetailRepository,
                              IOrderRepository orderRepository,
                              IProductOrderDetailRepository productOrderDetailRepository,
                              IProductRepository productRepository,
                              IProductTypeRepository productTypeRepository)
        {
            _logger = logger;
            this.orderDetailRepository = orderDetailRepository;
            this.orderRepository = orderRepository;
            this.productOrderDetailRepository = productOrderDetailRepository;
            this.productRepository = productRepository;
            this.productTypeRepository = productTypeRepository;
        }
        public IActionResult Index(string id)
        {
            var orders = orderRepository.Gets().ToList();
            var orderDetails = orderDetailRepository.Gets().ToList();
            var productOrderDetails = productOrderDetailRepository.Gets().ToList();
            var products = productRepository.Gets().ToList();
            var productTypes = productTypeRepository.Gets().ToList();
            if (id != null)
            {
                var dashboardView = (from o in orders
                                     join od in orderDetails on o.OrderId equals od.OrderId
                                     join pod in productOrderDetails on od.OrderDetailId equals pod.OrderDetailId
                                     join p in products on pod.ProductId equals p.ProductId
                                     join pt in productTypes on p.ProductTypeId equals pt.ProductTypeId
                                     where od.Paid == true &&
                                     (!string.IsNullOrEmpty(od.UserId) && string.Compare(od.UserId.Trim(), id) == 0)
                                     select new DashboardViewModel()
                                     {
                                         OrderID = o.OrderId,
                                         StartTime = o.StarTime.ToString(),
                                         EndTime = o.EndTime.ToString(),
                                         ProductName = p.Name,
                                         Count = pod.Count,
                                         Price = p.Price,
                                         Total = pod.Count * p.Price,
                                         UserId = od.UserId
                                     }).ToList();

                ViewBag.Total = (from dbv in dashboardView
                                 select dbv.Total).Sum();

                return View(dashboardView);
            }
            else
            {
                var dashboardView = (from o in orders
                                     join od in orderDetails on o.OrderId equals od.OrderId
                                     join pod in productOrderDetails on od.OrderDetailId equals pod.OrderDetailId
                                     join p in products on pod.ProductId equals p.ProductId
                                     join pt in productTypes on p.ProductTypeId equals pt.ProductTypeId
                                     where od.Paid == true
                                     select new DashboardViewModel()
                                     {
                                         OrderID = o.OrderId,
                                         StartTime = o.StarTime.ToString(),
                                         EndTime = o.EndTime.ToString(),
                                         ProductName = p.Name,
                                         Count = pod.Count,
                                         Price = p.Price,
                                         Total = pod.Count * p.Price,
                                         UserId = od.UserId
                                     }).ToList();

                ViewBag.Total = (from dbv in dashboardView
                                 select dbv.Total).Sum();

                return View(dashboardView);
            }
        
        }
    }
}
