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
using DAL.UOW;
using Microsoft.Extensions.Logging;
using LoggerSevice.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService service;
        private IMapper mapper;
        private readonly ILoggerManager logger;

        public AccountController(IAccountService service, ILoggerManager logger)
        {
            this.service = service;
            this.logger = logger;
            mapper = new MapperConfiguration(ctr => ctr.AddProfile(new API.Mapper.MapperSetAPI())).CreateMapper();
            //file = new DAL.Repository.FileRepository(new DAL.Context.ApplicationContext());
            
        }
        // GET: api/Account
        

        [HttpPost]
        [Route("Register")]
        public  IActionResult Register ([FromBody] ApplicationUserView model)
        {
            logger.LogInfo("Someone is trying to register in role User");
            

            if (ModelState.IsValid)
            {
                var user = mapper.Map<BL.ModelsDTO.ApplicationModels.ApplicationUserDTO>(model);
                user.Roles.Add("User");
                var result = service.CreateUser(user);
                if (result)
                {
                    logger.LogInfo("User was created");
                    return Ok(user);
                }
                else
                {
                    logger.LogError("Something went wrong with registration");
                    return Content("User wasn't created ");
                }
                //service.CreateUser()
            }
            else
            {
                 logger.LogError("Model is not valid");
                return NoContent();
            }

        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login([FromQuery] LoginModelView model)
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

      


        [HttpGet]
        [Authorize]
        [Route("Delete")]
        
        public IActionResult Delete([FromQuery] DeleteAccModelView model)
        {
            var delete = mapper.Map<BL.ModelsDTO.OtherModels.DeleteAccModel>(model);
            delete.Id = User.Identity.Name;
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
          [HttpGet]
          [Authorize]
          [Route("LogOut")]
          public IActionResult LogOut()
        {
             
            return Ok();
        }
       
    }
   public class MyClass
    {
        public string Id { get; set; }
        public int Name { get; set; }
    }
}
