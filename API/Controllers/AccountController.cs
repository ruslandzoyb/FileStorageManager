using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


using API.Models;
using AutoMapper;
using BL.Configuration.Mapper;
using BL.Services.CommonServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BL.Interfaces.OrdersInterfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService service;
        private IMapper mapper;

        public AccountController(IAccountService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(ctr => ctr.AddProfile(new API.Mapper.MapperSetAPI())).CreateMapper();
           
        }
        // GET: api/Account
        

        [HttpPost]
        [Route("Login")]
        public IActionResult Register ([FromBody] ApplicationUserView model)
        {


            if (ModelState.IsValid)
            {
                var user = mapper.Map<BL.ModelsDTO.ApplicationModels.ApplicationUserDTO>(model);
                user.Roles.Add("User");
                var result = service.CreateUser(user);
                if (result)
                {
                    return Ok(user);
                }
                else
                {
                    //todo:Ex
                    return Content("User wasn't created ");
                }
                //service.CreateUser()
            }
            else
            {
                //todo:Ex
                return NotFound();
            }

        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModelView model)
        {
            if (ModelState.IsValid)
            {
                var login = mapper.Map<BL.ModelsDTO.ApplicationModels.LoginModelDTO>(model);
                var message = service.Login(login);
                return Ok(message);
                
            }
            else
            {
                return NotFound();
            }
        }



        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public IActionResult Delete([FromBody] DeleteAccModelView model)
        {
            var delete = mapper.Map<BL.ModelsDTO.OtherModels.DeleteAccModel>(model);
            var message = service.DeleteAccount(delete);
            if (message!=null)
            {
                return  Ok(message);
            }
            else
            {
                return NotFound();
            }
        }
          
       
    }
}
