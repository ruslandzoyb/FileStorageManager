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
using BL.ModelsDTO.OtherModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
   [Authorize(Roles = "User,Admin")]
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
        [Route("GetFile")]
        public IActionResult GetFile(int? id)
        {
            string user_id = User.Identity.Name;
          var file= mapper.Map<FileModelView>(service.GetFile(id, user_id));
           
            return Ok(file);
        }

              
             
        [HttpGet]
        
      
        [Route("FileInfo")]
        public IActionResult FileInfo([FromForm] int?id)
        {
            var user_id = this.User.Identity.Name;

           var info= service.InfoByFile(id, user_id);
            return Ok(info);
        }

        
        [HttpGet]
        [Route("Files")]
     // [Authorize(Roles ="User")]
        public IActionResult GetFiles()
        {
            
            string user_id = User.Identity.Name;
            var files = mapper.Map<List<FileModelView>>(service.GetFiles(user_id).ToList());
            return Ok(files);
        }

       
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromForm]int? id)

        {
            var el = User.Identity.Name;
           var res= service.Delete(id, el);
           // service.Delete(id,);
            return Ok(res);
        }

        [HttpGet]
        [Route("ByLink")]
        public IActionResult GetByLink([FromQuery]string link)
        {
            var file = mapper.Map<FileModelView>(service.GetPath(link));
            return Ok(file);
        }

        [HttpPost]
        [Route("ChangeStatus")]
        
        public IActionResult ChangeStatus([FromBody]ChangeStatusModel model)
        {
           var view= service.ChangeStatus(model);

            return Ok(view);
        }

        [HttpGet]
        
        [Route("Download")]
        public FileResult Download([FromQuery] string link)
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
