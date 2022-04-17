using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Contracts;
using MyWeb.Models;

namespace MyWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalePersonsController : ControllerBase
    {
        private ISalePersonRepository _SalePersonRepository;

        public SalePersonsController(ISalePersonRepository SalePersonRepository)
        {
            _SalePersonRepository = SalePersonRepository;

        }

        [HttpGet]
        public IActionResult GetAllSalePersons()
        {
       
            return new ObjectResult(_SalePersonRepository.GetAll()) { StatusCode = 200 };


        }

        [HttpGet("{id}")]
        public IActionResult Find([FromRoute] int id)
        {
            if (_SalePersonRepository.IsExist(id))
            {
                var SalePerson = _SalePersonRepository.Find(id);
                return Ok(SalePerson);

            }
            return NotFound();

        }


        [HttpPost]
        public IActionResult PostSalePerson([FromBody]  SalePersons SalePerson)
        {
            if (ModelState.IsValid)
            {
                _SalePersonRepository.Add(SalePerson);
                return CreatedAtAction("GeTSalePersonById", new { id = SalePerson.SalePersonId }, SalePerson);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult PutSalePerson([FromRoute] int id, [FromBody] SalePersons SalePerson)
        {

            if (ModelState.IsValid)
            {
                _SalePersonRepository.Update(SalePerson);
                return Ok(SalePerson);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("id")]
        public IActionResult Delete([FromRoute] int id)
        {
            _SalePersonRepository.Delete(id);
            return Ok();


        }



    }
}
