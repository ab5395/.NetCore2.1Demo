using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemoSolution.Data.Classes;
using CoreDemoSolution.Repository.Interfaces;
using CoreDemoSolution.Repository.Models;
using CoreDemoSolution.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoSolution.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetProductList()
        {

            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10, 20, 50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                // getting all Customer data  
                var products = _productRepository.GetAll();
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    products = products.Where(x => x.Name.NullToString().ToLower().Contains(searchValue.ToLower())
                                                   || x.Description.NullToString().ToLower().Contains(searchValue.ToLower())
                                                        || x.Price.ToString().Contains(searchValue.ToLower()));
                }
                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {

                    switch (sortColumn)
                    {
                        case "Name":
                            products = sortColumnDirection.Contains("asc")? products.OrderBy(x => x.Name) : products.OrderByDescending(x => x.Name);
                            break;
                        case "Price":
                            products = sortColumnDirection.Contains("asc") ? products.OrderBy(x => x.Price) : products.OrderByDescending(x => x.Price);
                            break;
                        case "Description":
                            products = sortColumnDirection.Contains("asc") ? products.OrderBy(x => x.Description) : products.OrderByDescending(x => x.Description);
                            break;
                        default:
                            break;
                    }
                }


                //total number of rows counts   
                recordsTotal = products.Count();
                //Paging   
                var data = products.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> AddUpdateProduct(int? productId)
        {
            var productModel = new ProductModel();
            if (productId != null && productId > 0)
            {
                var product = await _productRepository.GetSingleAsync(x => x.ProductId == productId);
                productModel.ProductId = product.ProductId;
                productModel.Name = product.Name;
                productModel.Description = product.Description;
                productModel.Price = product.Price;
            }
            return PartialView("_SetProduct", productModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdateProduct(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var product = new Product();
            if (model.ProductId > 0)
            {
                product = await _productRepository.GetSingleAsync(x => x.ProductId == model.ProductId);
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
            }
            else
            {
                product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price
                };
                _productRepository.Add(product);
            }
            await _productRepository.SaveAsync();
            return new JsonResult(new
            {
                Status = true,
                Message = "Product Saved Successfully"
            });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetSingleAsync(x => x.ProductId == id);
            if (product != null)
            {
                _productRepository.Delete(product);
                await _productRepository.SaveAsync();
                return new JsonResult(new
                {
                    Status = true,
                    Message = "Product Deleted Successfully"
                });
            }
            return new JsonResult(new
            {
                Status = false,
                Message = "Product Not Deleted"
            });
        }
    }

    
}