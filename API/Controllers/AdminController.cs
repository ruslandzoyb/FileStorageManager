using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.CommonModels;
using AutoMapper;
using BL.Interfaces.OrdersInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdminService adminService;
        IMapper mapper;
        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
            mapper = new MapperConfiguration(ctr => ctr.AddProfile(new API.Mapper.MapperSetAPI())).CreateMapper();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromQuery]string id)
        {
            adminService.DeleteUser(id);
            return Ok();
        }

        [HttpGet]
        [Route("Files")]

        public IActionResult GetFiles()
        {
           var files=  mapper.Map<List<FileModelView>>(adminService.GetFiles());
            if (files!=null)
            {
                return Ok(files);
            }
            else
            {
                return NotFound("Users wast found");
            }
            
        }


        [HttpGet]
        [Route("Statuses")]
        public IActionResult GetStatuses()
        {
           var statuses=  mapper.Map<List<StatusViewModel>>(adminService.GetStatuses());
            if (statuses!=null)
            {
                return Ok(statuses);
            }
            else
            {
                return NotFound("Statuses wasnt found");
            }
            
        }

        [HttpGet]
        [Route("Types")]

        public IActionResult GetTypes()
        {
            var types = mapper.Map<List<TypeVievModel>>(adminService.GetTypes());
            if (types!=null)
            {
                return Ok(types);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpGet]
        [Route("User")]
        public IActionResult GetUser([FromQuery] string id)
        {
            var user = mapper.Map<UserViewModel>( adminService.GetUser(id));
            return Ok(user);
        }

        [HttpGet]
        [Route("UserInfo")]

        public IActionResult GetUserInfo([FromQuery] string id)
        {
            var info = adminService.GetUserInfo(id);
            if (info!=null)
            {
                return Ok(info);
            }
            return NotFound();
        }
        
        [HttpGet]
        [Route("IdentityUsers")]
        public  async Task<IActionResult> GetIdenities()
        {
           var users= await adminService.GetIdentityUsers();
            return  Ok(users);
        }

        [HttpGet]
        [Route("Users")]
        public  async Task<IActionResult> GetUsers()
        {
            var users = await mapper.Map<Task<List<UserViewModel>>>( adminService.GetUsers());
            return Ok(users);
            
        }
    }
}
