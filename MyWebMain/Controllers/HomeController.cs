using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebMain.Models;
using MyWebMain.Repositories;

namespace MyWebMain.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        CustomerRepository customerRepository;
        public HomeController()
        {
            customerRepository = new CustomerRepository();

        }



        public IActionResult Index()
        {

            var token = User.FindFirst("AccsessToken").Value;

            return View(customerRepository.GetAllCustomers(token));
        }

        [Route("[controller]/AddCustomer")]
        [HttpGet]
        public IActionResult AddCustomer()
        {


            return View();
        }
        [Route("[controller]/AddCustomer")]
        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            var token = User.FindFirst("AccsessToken").Value;
            customerRepository.InsertCustomer(customer,token);


           return RedirectToAction("Index");
        }


        [Route("[controller]/UpdateCustomer/{id}")]
        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            var token = User.FindFirst("AccsessToken").Value;
            var customer= customerRepository.GetCustomerById(id,token);

            return View(customer);
        }
        [Route("[controller]/UpdateCustomer/{id}")]
        [HttpPost]
        public IActionResult UpdateCustomer(Customer customer )
        {
            var token = User.FindFirst("AccsessToken").Value;
            customerRepository.UpdateCustomer(customer,token);

            return RedirectToAction("Index");
        }


        [Route("[controller]/DeleteCustomer/{id}")]
        public IActionResult DeleteCustomer( int id)
        {
            var token = User.FindFirst("AccsessToken").Value;
            customerRepository.DeleteCustomer(id,token);

            return RedirectToAction("Index");
        }










    }
}
    
    

