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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService service;
        private IMapper mapper;
        private UnitOfWork unit;
        public AccountController(IAccountService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(ctr => ctr.AddProfile(new API.Mapper.MapperSetAPI())).CreateMapper();
            //file = new DAL.Repository.FileRepository(new DAL.Context.ApplicationContext());
            unit = new UnitOfWork(new DAL.Context.ApplicationContext());
        }
        // GET: api/Account
        

        [HttpPost]
        [Route("Register")]
        public IActionResult Register ([FromForm] ApplicationUserView model)
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
                return NoContent();
            }

        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login([FromForm] LoginModelView model)
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

        //[HttpGet]
        //[Route("Hi")]
        //public string Get()
        //{
        //    return "Hello bitch";
        //}


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
          
       [HttpGet]
       [Route("Get")]
       public IActionResult Post()
        {
            var file = "";
            return Ok(file);
        }
    }
   public class MyClass
    {
        public string Id { get; set; }
        public int Name { get; set; }
    }
}
