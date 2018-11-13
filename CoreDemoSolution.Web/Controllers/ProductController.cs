using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemoSolution.Data.Classes;
using CoreDemoSolution.Repository.Interfaces;
using CoreDemoSolution.Repository.Models;
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
            var products = _productRepository.GetAll().ToList();
            return View(products);
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
        public async Task<IActionResult> DeleteProduct([FromQuery]int id)
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