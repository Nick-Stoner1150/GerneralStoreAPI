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
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostTransaction([FromBody] Transaction model)
        {
            if(model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            if(ModelState.IsValid)
            {
                if(model.Product.IsInStock == true && model.ItemCount <= model.Product.NumberInInventory)
                {
                    _context.Transactions.Add(model);
                    await _context.SaveChangesAsync();
                    

                    return Ok("You created the Transaction!");
                }

                if(model.Product.IsInStock == false)
                {
                    return BadRequest("That product is out of stock!");
                }

                if(model.Product.IsInStock == true && model.ItemCount > model.Product.NumberInInventory)
                {
                    return BadRequest("There are not enough items in stock for that purchase!");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Transaction> transactions = await _context.Transactions.ToListAsync();
            return Ok(transactions);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Transaction transaction = await _context.Transactions.FindAsync(id);

            if(transaction != null)
            {
                return Ok(transaction);
            }

            return NotFound();
        }
    }
}
