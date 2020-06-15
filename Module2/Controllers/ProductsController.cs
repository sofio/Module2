using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Module2.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        static List<Product> _products = new List<Product>()
        {
            new Product(){ProductId=0, ProductName= "Laptop", ProductPrice= "200" },
            new Product(){ProductId=1, ProductName= "SmartPhone", ProductPrice= "100" }
        };


        private Data.ProductsDbContext _productsDbContext;

        public ProductsController(Data.ProductsDbContext productsDbContext)
        {
            _productsDbContext = productsDbContext;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productsDbContext.Products;
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var product = _productsDbContext.Products.SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound("Not Found");
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            //  _products.Add(product);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productsDbContext.Products.Add(product);
            _productsDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            //_products[id] = product;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                _productsDbContext.Products.Update(product);
                _productsDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound("Not Founf ");
            }

            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // _products.RemoveAt(id);

            var product = _productsDbContext.Products.SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound("Not Found");
            }
            _productsDbContext.Products.Remove(product);
            _productsDbContext.SaveChanges();
            return Ok("Product Deleited");
        }
    }
}