using GerneralStoreAPI.Models;
using GerneralStoreAPI.Models.Models.POCOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GerneralStoreAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        // POST (create) 
        //api/Customer
        [HttpPost]
        public async Task<IHttpActionResult> PostCustomer([FromBody] Customer model)
        {
            if(model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            if(ModelState.IsValid)
            {
                _context.Customers.Add(model);
                await _context.SaveChangesAsync();

                return Ok("You created the Customer!");
            }
            return BadRequest(ModelState);
        }


        // Get ALL 
        // api/Customer
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        // GET By ID
        // api/Customer/{id}
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            
            if(customer != null)
            {
                return Ok(customer);
            }

            return NotFound();
        }

        // PUT (update)
        // api/Customer/{id}
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer([FromUri] int id, [FromBody] Customer updatedCustomer)
        {
            if(id != updatedCustomer.Id)
            {
                return BadRequest("Id's do not match.");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Customer customer = await _context.Customers.FindAsync(id);

            if (customer is null)
                return NotFound();

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;

            await _context.SaveChangesAsync();

            return Ok("The customer was udapted");

        }

    }
}
