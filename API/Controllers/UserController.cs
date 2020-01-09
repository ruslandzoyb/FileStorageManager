using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BL.Interfaces.OrdersInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        IUserService service;
        IMapper mapper;

        public UserController(IUserService service)
        {
            this.service = service;
        }
      
        [HttpGet]
        public IActionResult GetFile(int? id)
        {
            //var file = mapper.Map < File >
            return Ok();
        }

        
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

     
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

       
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
