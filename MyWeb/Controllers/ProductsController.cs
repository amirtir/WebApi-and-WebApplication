using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWeb.Contracts;
using MyWeb.Models;

namespace MyWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Products> GetProducts()
        {
            return _productRepository.GetAll();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult GetProducts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _productRepository.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult PutProducts([FromRoute] int id, [FromBody] Products product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != product.ProductId)
            //{
            //    return BadRequest();
            //}
      
            return Ok(_productRepository.Update(product));

        }

  
        // POST: api/Products
        [HttpPost]
        public  IActionResult PostProducts([FromBody] Products product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productRepository.Add(product);


            return CreatedAtAction("GetProducts", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public  IActionResult DeleteProducts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_productRepository.Delete(id));
        }

        private bool ProductsExists(int id)
        {
            return _productRepository.IsExist(id);
        }
    }
}