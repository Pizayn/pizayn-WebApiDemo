using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.DataAccess;
using WebApiDemo.Entities;
using WebApiDemo.Model;

namespace WebApiDemo.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private IProductDal _productDal;

        public ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [HttpGet("")]
        public IActionResult Get()
        {
            var products = _productDal.GetList();
            return Ok(products);
        }
        [HttpGet("{productId}")]
        public IActionResult Get(int productId)
        {
            try
            {
                var product = _productDal.Get(x => x.ProductId == productId);
                if (product == null)
                {
                    return NotFound($"There is no product  with id= {productId}");
                }

                return Ok(product);

            }
            catch
            {

            }

            return BadRequest();


        }

        public IActionResult Post(Product product)
        {
            try
            {

                _productDal.Add(product);
                return new StatusCodeResult(201);
            }
            catch { }

            return BadRequest();
        }
        [HttpPut]
        public IActionResult Put(Product product)
        {
            try
            {
                _productDal.Update(product);
                return Ok(product);
            }
            catch
            {

            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(Product product)
        {
            try
            {
                _productDal.Delete(product);
                return Ok();
            }
            catch
            {

            }

            return BadRequest();
        }
        [HttpGet("GetProductWithDetails")]
        public List<ProductModel> GetProductWithDetails()
        {
            List<ProductModel> model2 = new List<ProductModel>();
              
           
            model2 = _productDal.GetProductWithDetails();

            return model2;


         


        }
    }
}