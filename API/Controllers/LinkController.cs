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
    [AllowAnonymous]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private ILinkService service;
        private IMapper mapper;
        public LinkController(ILinkService service)
        {
            this.service = service;
            mapper = new MapperConfiguration(ctr => ctr.AddProfile(new API.Mapper.MapperSetAPI())).CreateMapper();
        }

        [HttpGet]
        [Route("GetPath")]
        public IActionResult GetPath([FromQuery] string link)
        {
          var file= mapper.Map<FileModelView>(service.GetPath(link));
            return Ok(file);
        }


    }
}
