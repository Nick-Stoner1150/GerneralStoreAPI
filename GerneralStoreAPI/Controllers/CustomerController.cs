using GerneralStoreAPI.Models.Models.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GerneralStoreAPI.Controllers
{
    public class CustomerController : ApiController
    {
        // POST (create) 
        //api/Customer
        [HttpPost]
        public async Task<IHttpActionResult> PostCustomer([FromBody] Customer model)
        {
            
        }


    }
}
