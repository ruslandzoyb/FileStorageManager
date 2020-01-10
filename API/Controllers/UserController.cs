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
        
       //[Authorize(Roles ="User")]
        [Route("Go")]
        public IActionResult FileInfo()
        {
            var el = this.User.Identity.Name;

          // var info= service.InfoByFile(id, user_id);
            return Ok(el);
        }

        
        [HttpGet]
        [Route("Files")]
     // [Authorize(Roles ="User")]
        public IActionResult GetFiles()
        {
           // int user_id = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var files = mapper.Map<List<FileModelView>>(service.GetList().ToList());
            return Ok(files);
        }

       
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var el = User.Identity.Name;
           // service.Delete(id,);
            return Ok("Deleted");
        }

        [HttpGet]
        [Route("ByLink")]
        public IActionResult GetByLink(string link)
        {
            var file = mapper.Map<FileModelView>(service.GetPath(link));
            return Ok(file);
        }


        [HttpGet]
        //[Authorize]
        [Route("Download")]
        public FileResult Download([FromRoute] string link)
        {
            var download = mapper.Map<DownloadViewModel>(service.Download(link));
            if (download!=null)
            {
                return File(download.Array, download.Type, download.Name);
            }
            else
            {
                throw new Exception();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("Upload")]
        public IActionResult Upload([FromForm] UploadFileViewModel model)
        {
            
            var upload = mapper.Map<BL.ModelsDTO.OtherModels.FileUploadModel>(model);
            upload.UserId = User.Identity.Name;
            service.Upload(upload);
            return Ok();
        }
    }
  
}
