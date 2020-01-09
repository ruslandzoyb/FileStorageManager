using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;
using BL.Interfaces.OrdersInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        IRoleService service;
        IMapper mapper;

        public RoleController(IRoleService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(ctr => ctr.AddProfile(new API.Mapper.MapperSetAPI())).CreateMapper();
        }

        [HttpPost]
        
        public IActionResult AddRole([FromBody] string role)
        {
            service.AddRole(role);
            return Ok();
        }


        [HttpDelete]
        public IActionResult Delete([FromBody] string role)
        {
            service.Delete(role);
            return Ok();
        }

        [HttpGet]
       public IActionResult GetRoles()
        {
            return Ok(mapper.Map<RoleViewModel>(service.GetRoles()));
        }

        [HttpGet]
        public IActionResult GetRoles([FromBody] string role)
        {
          return Ok(mapper.Map<ApplicationUserView>(service.GetUsersByRole(role)));
        }
        
       

       

    }
}
