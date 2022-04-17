using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Contracts;
using MyWeb.Models;

namespace MyWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CustomersController : ControllerBase
    {
        private ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
           _customerRepository=customerRepository;

        }

        [HttpGet]
        //[ResponseCache(Duration =60)]
        public IActionResult GetAllCustomers()
        {
            Request.HttpContext.Response.Headers.Add("Count",_customerRepository.Count().ToString());
            Request.HttpContext.Response.Headers.Add("Author", "Amir Tir");

            return new ObjectResult(_customerRepository.GetAll()) { StatusCode=200};

     
        }

        [HttpGet("{id}")]
        public IActionResult GeTCustomerById([FromRoute] int id)
        {
            if (_customerRepository.IsExist(id))
            {
                var customer = _customerRepository.Find(id);
                return Ok(customer);

            }
            return NotFound();

        }


        [HttpPost]
        public IActionResult PostCustomer([FromBody]  Customers customer)
        {
            if(ModelState.IsValid)
            {
                _customerRepository.Add(customer);
                return CreatedAtAction("GeTCustomerById", new { id = customer.CustomerId }, customer);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
    public IActionResult PutCustomer( [FromRoute] int id, [FromBody] Customers customer)
        {

            if(ModelState.IsValid)
            {
                _customerRepository.Update(customer);
                return Ok(customer);
            }
            return BadRequest(ModelState);
        }

       [HttpDelete("{id}")]
        public IActionResult DeleteCustomer([FromRoute] int id)
        {
            _customerRepository.Delete(id);
            return Ok();


        }
    }
}