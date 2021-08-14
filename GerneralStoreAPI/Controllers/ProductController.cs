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
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostProduct([FromBody] Product model)
        {
            if(model is null)
            {
                return BadRequest("Your request body cannot be empty!");
            }

            if(ModelState.IsValid)
            {
                _context.Products.Add(model);
                await _context.SaveChangesAsync();

                return Ok("You created a product!");
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] string sku)
        {
            Product product = await _context.Products.FindAsync(sku);

            if(product != null)
            {
                return Ok(product);
            }

            return NotFound();
        }


    }

}
