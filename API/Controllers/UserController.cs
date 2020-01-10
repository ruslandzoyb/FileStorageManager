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
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
   // [Authorize(Roles = "User,Admin")]
    [ApiController]
    public class UserController : ControllerBase
    {

       protected IUserService service;
       protected IMapper mapper;

        public UserController(IUserService service)
        {
           
            this.service = service;
            mapper = new MapperConfiguration(ctr => ctr.AddProfile(new API.Mapper.MapperSetAPI())).CreateMapper();
        }
      
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult GetFile(int? id)
        {
           int user_id = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
          var file= mapper.Map<FileModelView>(service.GetFile(id, user_id));
            //var file = mapper.Map < File >
            return Ok(file);
        }

              
             
        [HttpGet]
        
       [Authorize(Roles ="Admin")]
        [Route("Go")]
        public IActionResult FileInfo()
        {
           // int user_id = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

           //var info= service.InfoByFile(id, user_id);
            return Ok("Hello suka");
        }

        
        [HttpGet]
        [Route("Files")]
      [Authorize(Roles ="User")]
        public IActionResult GetFiles()
        {
           // int user_id = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var files = mapper.Map<List<FileModelView>>(service.GetList().ToList());
            return Ok(files);
        }

       
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            int user_id = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            service.Delete(id, user_id);
            return Ok("Deleted");
        }

        [HttpGet]
        [Route("ByLink")]
        public IActionResult GetByLink(string link)
        {
            var file = mapper.Map<FileModelView>(service.GetPath(link));
            return Ok(file);
        }

        [HttpPost]
        public IActionResult Upload([FromForm] UploadFileViewModel model)
        {
            var upload = mapper.Map<BL.ModelsDTO.OtherModels.FileUploadModel>(model);
            service.Upload(upload);
            return Ok();
        }
    }
  
}
