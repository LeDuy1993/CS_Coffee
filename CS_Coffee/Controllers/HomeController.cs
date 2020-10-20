using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CS_Coffee.Models;
using Microsoft.AspNetCore.Authorization;
using CS_Coffee.Repository;
using CS_Coffee.OrderModels;
using Microsoft.AspNetCore.Identity;

namespace CS_Coffee.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductOrderDetailRepository productOrderDetailRepository;
        private readonly IProductRepository productRepository;
        private readonly IProductTypeRepository productTypeRepository;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly CoffeeContext context;

        public HomeController(ILogger<HomeController> logger,
                              IOrderDetailRepository orderDetailRepository,
                              IOrderRepository orderRepository,
                              IProductOrderDetailRepository productOrderDetailRepository,
                              IProductRepository productRepository,
                              IProductTypeRepository productTypeRepository,
                               SignInManager<ApplicationUser> signInManager,
                               UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                CoffeeContext context
                              )
        {
            _logger = logger;
            this.orderDetailRepository = orderDetailRepository;
            this.orderRepository = orderRepository;
            this.productOrderDetailRepository = productOrderDetailRepository;
            this.productRepository = productRepository;
            this.productTypeRepository = productTypeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public IActionResult Index(int id, int pid)
        {
            var products = productRepository.Gets().ToList();
            var orderDetails = orderDetailRepository.Gets().ToList();
            var orders = orderRepository.Gets().ToList();
            var productOrderDetails = productOrderDetailRepository.Gets().ToList();
            var productTypes = productTypeRepository.Gets().ToList();
            ViewBag.Types = productTypes;
            ViewBag.ProductTypeID = pid;
            ViewBag.Orders = orders;
            ViewBag.Products = products;
            if (id != 0)
            {
                var checkOrderId = (from od in orders
                                    where od.OrderId == id
                                    select od.OrderId).FirstOrDefault();
                if (checkOrderId != 0)
                {
                    ViewBag.OrderUse = orderRepository.Get(id).Using;
                    ViewBag.TableID = checkOrderId;
                    var newOrderView = new List<OrderViewModel>();
                    newOrderView = (from o in orders
                                    join od in orderDetails on o.OrderId equals od.OrderId
                                    join pod in productOrderDetails on od.OrderDetailId equals pod.OrderDetailId
                                    join p in products on pod.ProductId equals p.ProductId
                                    where od.OrderId == id && od.Paid == false
                                    select new OrderViewModel()
                                    {
                                        Name = p.Name,
                                        Price = p.Price,
                                        Count = pod.Count,
                                        Total = p.Price * pod.Count,
                                        PodId = pod.ProductOrderDetailId,
                                        OrderId = od.OrderId,
                                        StartTime = o.StarTime
                                    }).ToList();
                    ViewBag.Sum = newOrderView.AsEnumerable().Sum(t => t.Total);
                    var checkOrderUsing = (from od in orders
                                           where od.OrderId == id
                                           select od.Using).FirstOrDefault();
                    if (checkOrderUsing)
                    {
                        ViewBag.StartTime = (from nod in newOrderView
                                             where nod.OrderId == id
                                             select nod.StartTime).FirstOrDefault().ToString("hh:mm:ss");
                    }
                    else
                    {
                        ViewBag.StartTime = null;
                    }

                    return View(newOrderView);
                }
                else
                {
                   
                    return RedirectToAction("Index", "Home");
                }

            }
            return View();
        }
        public IActionResult GetPartial(int id)
        {
            var products = productRepository.Gets().ToList();
            if (id == 0)
            {
                return PartialView("_product", products);
            }
            else
            {
                var newproducts = products.FindAll(p => p.ProductTypeId == id).ToList();
                return PartialView("_product", newproducts);
            }
          
            
        }
        /*        [Authorize(Roles = "Boss,Leader,Staff")]*/
        [HttpGet]
        public IActionResult Edit(int id, string returnUrl)
        {
            var userID = Task.Run(async () => await userManager.FindByNameAsync(User.Identity.Name)).Result.Id;
            var rolecheck = (from role in context.UserRoles.ToList()
                             where role.UserId == userID
                             select role).FirstOrDefault();

            if (rolecheck != null)
            {
                ViewBag.ReturnUrl = returnUrl;
                var productOrderDetail = productOrderDetailRepository.Get(id);
                var od = new OrderViewModel();
                if (productOrderDetail != null)
                {
                    od.Name = productRepository.Get(productOrderDetail.ProductId).Name;
                    od.Price = productRepository.Get(productOrderDetail.ProductId).Price;
                    od.PodId = productOrderDetail.ProductOrderDetailId;
                    od.Count = productOrderDetail.Count;
                    od.ProductId = productOrderDetail.ProductId;
                    od.OrderDetailId = productOrderDetail.OrderDetailId;
                    od.ProductOrderDetailId = id;
                    od.OrderId = orderDetailRepository.Get(productOrderDetailRepository.Get(id).OrderDetailId).OrderId;
                    od.Total = productRepository.Get(productOrderDetail.ProductId).Price * productOrderDetail.Count;
                    return View(od);
                }
                else
                {
                    return RedirectToAction("PageNotFound", "Error");
                }
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account");

            }

        }
        [Authorize(Roles = "Boss,Leader,Staff")]
        [HttpPost]
        public IActionResult Edit(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var productOD = new ProductOrderDetail()
                {
                    ProductId = model.ProductId,
                    OrderDetailId = model.OrderDetailId,
                    Count = model.Count,
                    ProductOrderDetailId = model.PodId
                };
                if (productOrderDetailRepository.Edit(productOD) != null)
                {
                    var url = "https://localhost:44366/Home/Index/" + model.OrderId;
                    return Redirect(url);
                  
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize(Roles = "Boss,Leader,Staff")]
        [HttpGet]
        public IActionResult Order(int id, int pid)
        {
            var userID = Task.Run(async () => await userManager.FindByNameAsync(User.Identity.Name)).Result.Id;
            var rolecheck = (from role in context.UserRoles.ToList()
                             where role.UserId == userID
                             select role).FirstOrDefault();

            if (rolecheck != null)
            {
                if (id != 0)
                {
                    var table = orderRepository.Get(id);
                    var product = productRepository.Get(pid);

                    var od = new OrderDetail();
                    od.OrderId = table.OrderId;
                    if (orderDetailRepository.Create(od) != null)
                    {
                        var creatOd = new OrderCreateViewModel();

                        creatOd.TableId = id;
                        creatOd.Using = table.Using;
                        creatOd.OrderDetailId = (from odr in orderDetailRepository.Gets()
                                                 select odr.OrderDetailId).Max();
                        if (productOrderDetailRepository.Gets().Count() != 0)
                        {
                            creatOd.ProductOrderDetailId = (from podr in productOrderDetailRepository.Gets()
                                                            select podr.ProductOrderDetailId).Max() + 1;
                        }
                        else
                        {
                            creatOd.ProductOrderDetailId = 1;
                        }

                        creatOd.Name = product.Name;
                        creatOd.Count = 1;
                        creatOd.ProductId = pid;

                        return View(creatOd);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        [Authorize(Roles = "Boss,Leader,Staff")]
        [HttpPost]
        public IActionResult Order(OrderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order()
                {
                    OrderId = model.TableId,
                    Using = true,
                    StarTime = DateTime.Now,
                    EndTime = DateTime.Now
                };
                var result = orderRepository.Edit(order);
                var pod = new ProductOrderDetail()
                {
                    ProductId = model.ProductId,
                    OrderDetailId = model.OrderDetailId,
                    ProductOrderDetailId = model.ProductOrderDetailId,
                    Count = model.Count
                };
                var result2 = productOrderDetailRepository.Create(pod);
                if (result != null && result2 != null)
                {
                    var url = "https://localhost:44366/Home/Index/" + model.TableId;
                    return Redirect(url);

                }


            }
            return View();
        }
        [Authorize(Roles = "Boss,Leader,Staff")]
        public IActionResult Delete(int pid, int id)
        {
            var productOD = productOrderDetailRepository.Get(id);
            var orderDetail = orderDetailRepository.Get(productOD.OrderDetailId);
            var delPOD = productOrderDetailRepository.Delete(id);
            var delOD = orderDetailRepository.Delete(orderDetail.OrderDetailId);
            if (delOD && delPOD)
            {
                var orderDetailCheck = (from od in orderDetailRepository.Gets()
                                        where od.OrderId == pid && od.Paid == false
                                        select od).FirstOrDefault();
                if (orderDetailCheck != null)
                {
                    var url = "https://localhost:44366/Home/Index/" + orderDetail.OrderId;
                    return Redirect(url);
                }
                else
                {
                    var order = orderRepository.Get(pid);
                    order.Using = false;
                    var result = orderRepository.Edit(order);
                    if (result != null)
                    {
                        var url = "https://localhost:44366/Home/Index/" + orderDetail.OrderId;
                        return Redirect(url);
                     
                    }
                }
            }
            return View();
        }
        [Authorize(Roles = "Boss,Leader,Staff")]
        public IActionResult Bill(int id)
        {
            var userID = Task.Run(async () => await userManager.FindByNameAsync(User.Identity.Name)).Result.Id;
            var rolecheck = (from role in context.UserRoles.ToList()
                             where role.UserId == userID
                             select role).FirstOrDefault();

            if (rolecheck != null)
            {
                var order = orderRepository.Get(id);
                order.EndTime = DateTime.Now;
                order.Using = false;

                var resultO = orderRepository.Edit(order);
                var orderdetails = (from ods in orderDetailRepository.Gets()
                                    where ods.OrderId == id
                                    select ods).ToList();
                foreach (var od in orderdetails)
                {
                    od.Paid = true;
                    od.UserId = userID;
                    orderDetailRepository.Edit(od);
                }
                if (resultO != null)
                {
                    var url = "https://localhost:44366/Home/Index/" + id;
                    return Redirect(url);
                
                }
                return View();
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        public IActionResult Change(int id, int pid)
        {
            var userID = Task.Run(async () => await userManager.FindByNameAsync(User.Identity.Name)).Result.Id;
            var rolecheck = (from role in context.UserRoles.ToList()
                             where role.UserId == userID
                             select role).FirstOrDefault();
            if (rolecheck != null)
            {
                var orderDetails = (from od in orderDetailRepository.Gets().ToList()
                                    where od.OrderId == id
                                    select od).ToList();
                foreach (var od in orderDetails)
                {
                    if (od.OrderId == id && od.Paid == false)
                    {
                        od.OrderId = pid;
                    }
                }
                var result = orderDetailRepository.Edits(orderDetails);
                if (result != null)
                {
                    var orderFrom = (from o in orderRepository.Gets()
                                     where o.OrderId == id
                                     select o).FirstOrDefault();
                    orderFrom.Using = false;
                    var orderTo = (from o in orderRepository.Gets()
                                   where o.OrderId == pid
                                   select o).FirstOrDefault();
                    orderTo.Using = true;
                    var resultFrom = orderRepository.Edit(orderFrom);
                    var resultTo = orderRepository.Edit(orderTo);
                    if (resultTo != null && resultFrom != null)
                    {
                        var url = "https://localhost:44366/Home/Index/" + id;
                        return Redirect(url);
                      
                    }
                }
                return View();
            }
            return RedirectToAction("AccessDenied", "Account");
        }
    }
}
