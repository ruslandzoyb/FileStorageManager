using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // GET: api/Test
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    // return new string[] { "value1", "value2" };

        //    r
           
        //}

        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
           
            HttpContext.Request.Form.Files[].Headers.
            return "value";
        }

        // POST: api/Test
        [HttpPost]
        public string Post([FromBody] string value)
        {
            var coll = HttpContext.Request.Form.Files["file"];
           Response.SendFileAsync
            return value;
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            byte[] byffer = new byte[256];
            Request.Body.Read(byffer, 1, 50);
            
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
