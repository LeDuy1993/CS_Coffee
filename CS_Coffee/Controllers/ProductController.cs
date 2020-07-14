using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CS_Coffee.Models;
using CS_Coffee.ProductModels;
using CS_Coffee.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CS_Coffee.Controllers
{
    [Authorize(Roles = "Boss")]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductTypeRepository productTypeRepository;

        public ProductController(IProductRepository productRepository,
                                IWebHostEnvironment webHostEnvironment,
                                IProductTypeRepository productTypeRepository)
        {
            this.productRepository = productRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.productTypeRepository = productTypeRepository;
        }
        private List<ProductType> GetProductTypes()              //1. getType
        {
            return productTypeRepository.Gets().ToList();
        }


        public ViewResult Index(int id)
        {
            var products = productRepository.Gets();
            ViewBag.ProductTypes = GetProductTypes();

            var types = GetProductTypes();
            var indexProducts = new List<ProductIndexViewModel>();
            if (id != 0)
            {
                indexProducts = (from t in types
                                 join pt in products on t.ProductTypeId equals pt.ProductTypeId
                                 where t.ProductTypeId == pt.ProductTypeId && pt.ProductTypeId == id
                                 select new ProductIndexViewModel()
                                 {
                                     ProductId = pt.ProductId,
                                     Name = pt.Name,
                                     AvatarPath = pt.AvatarPath,
                                     Price = pt.Price,
                                     TypeName = t.TypeName
                                 }).ToList();
            }
            else
            {
                indexProducts = (from t in types
                                 join pt in products on t.ProductTypeId equals pt.ProductTypeId
                                 where t.ProductTypeId == pt.ProductTypeId
                                 select new ProductIndexViewModel()
                                 {
                                     ProductId = pt.ProductId,
                                     Name = pt.Name,
                                     AvatarPath = pt.AvatarPath,
                                     Price = pt.Price,
                                     TypeName = t.TypeName
                                 }).ToList();
            }
            return View(indexProducts);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ProductTypes = GetProductTypes();            //1. Get Type
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    Name = model.Name,
                    ProductTypeId = model.ProductTypeId,
                    Price = model.Price
                };
                var fileName = string.Empty;
                if (model.Avatar != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    fileName = $"{Guid.NewGuid()}_{model.Avatar.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Avatar.CopyTo(fs);
                    }
                }
                product.AvatarPath = fileName;
                var newEmp = productRepository.Create(product);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.ProductTypes = GetProductTypes();
            var product = productRepository.Get(id);
            if (product == null)
            {
                return View("~/views/error/PageNotFound.cshtml", id);
            }
            var editProduct = new ProductEditViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                AvatarPath = product.AvatarPath,
                ProductId = product.ProductId,
                ProductTypeId = product.ProductTypeId

            };
            return View(editProduct);
        }
        public IActionResult Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Price = model.Price,
                    AvatarPath = model.AvatarPath,
                    ProductId = model.ProductId,
                    ProductTypeId = model.ProductTypeId
                };
                var fileName = string.Empty;
                if (model.Avatar != null)
                {
                    string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    fileName = $"{Guid.NewGuid()}_{model.Avatar.FileName}";
                    var filePath = Path.Combine(uploadFolder, fileName);
                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Avatar.CopyTo(fs);
                    }
                    product.AvatarPath = fileName;
                    if (!string.IsNullOrEmpty(model.AvatarPath))
                    {
                        string delFile = Path.Combine(webHostEnvironment.WebRootPath, "images", model.AvatarPath);
                        System.IO.File.Delete(delFile);
                    };

                }
                if (productRepository.Edit(product) != null)
                {
                    return RedirectToAction("Index", "Product");
                }
            }

            return View();
        }
        public IActionResult Delete(int id)
        {
            if (productRepository.Delete(id))
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
