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
        [Route("AddRole")]
        public IActionResult AddRole([FromQuery] string role)
        {
            service.AddRole(role);
            return Ok();
        }


        [HttpDelete]
        [Route("DeleteRole")]
        public IActionResult Delete([FromQuery] string role)
        {
            service.Delete(role);
            return Ok();
        }

        [HttpGet]
        [Route("Roles")]
       public IActionResult GetRoles()
        {
            return Ok(mapper.Map<List<RoleViewModel>>(service.GetRoles()));
        }

        [HttpGet]
        [Route("Users")]
        public IActionResult Users([FromQuery] string role)
        {
          return Ok(mapper.Map<List<ApplicationUserView>>(service.GetUsersByRole(role)));
        }
        
       

       

    }
}
