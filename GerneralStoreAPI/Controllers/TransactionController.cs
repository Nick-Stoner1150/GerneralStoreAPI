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
            // Check if model is null 
            if(model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            // Check if model state is invalid 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _context.Customers.FindAsync(model.CustomerId);
            if (customer is null)
                return BadRequest($"There is no customer with the Id of {model.CustomerId}. You must first create the customer.");

            // Find the product by the model.ProductSKU and see that it exists
            var productEntity = await _context.Products.FindAsync(model.ProductSKU);
            if (productEntity is null)
                return BadRequest($"The target Product with the SKU of {model.ProductSKU} does not exist");
            if (productEntity.IsInStock == false)
                return BadRequest($"Sorry the target Product with the SKU of {model.ProductSKU} is currently out of stock");
            if (model.ItemCount > productEntity.NumberInInventory)
                return BadRequest($"There are not enough items in stock. We only have {productEntity.NumberInInventory} in stock.");


            // create the transaction
            _context.Transactions.Add(model);
            productEntity.NumberInInventory = productEntity.NumberInInventory - model.ItemCount;

            await _context.SaveChangesAsync();
                return Ok($"{customer.FullName} purchased {model.ItemCount} of {productEntity.Name}");
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
