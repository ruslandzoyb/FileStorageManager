using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models.CommonModels;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using BL.Interfaces.OrdersInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

       private IUserService service;
       private IMapper mapper;

        public UserController(IUserService service)
        {
           
            this.service = service;
            mapper = new MapperConfiguration(ctr => ctr.AddProfile(new API.Mapper.MapperSetAPI())).CreateMapper();
        }
      
        [HttpGet]
        [Authorize(Roles ="User,Admin")]
        public IActionResult GetFile(int? id)
        {
           int user_id = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
          var file= mapper.Map<FileModelView>(service.GetFile(id, user_id));
            //var file = mapper.Map < File >
            return Ok(file);
        }

              
             
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult FileInfo(int ?id)
        {
            int user_id = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

           var info= service.InfoByFile(id, user_id);
            return Ok(info);
        }

        
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetFiles()
        {
            int user_id = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var files = service.GetFiles(user_id);
            return Ok(files);
        }

       
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int user_id = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            service.Delete(id, user_id);
            return Ok("Deleted");
        }
    }
  
}
