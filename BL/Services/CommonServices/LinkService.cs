using AutoMapper;
using BL.Configuration.Mapper;
using BL.Interfaces.OrdersInterfaces;
using BL.ModelsDTO.FileManageDTO;
using DAL.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using BL.ModelsDTO.OtherModels;

namespace BL.Services.CommonServices
{
    public class LinkService : ILinkService
    {
        IUnitOfWork database;
        private IMapper mapper;
        public LinkService(IUnitOfWork database)
        {
            this.database = database;
            mapper = new MapperConfiguration(ctg => ctg.AddProfile(new MapperSet())).CreateMapper();
        }
        public FileDownloadModel Download(string link)
        {

            var file = mapper.Map<FileDTO>(database.Links.Get(x => x.Code == link).Result.File);
            if (file!=null)
            {
                var download = new FileDownloadModel()
                {
                    Array = System.IO.File.ReadAllBytes(file.Path.Link),
                    Name = file.Name,
                    Type = file.Type.Format
                };
                //todo :Ex
                return download != null ? download : throw new Exception();
            }
            else
            {
                //todo :Ex
                throw new Exception();
            }
            }


        public FileDTO GetPath(string link)
        {
            if (link!=null)
            {
                var file = mapper.Map<FileDTO>(database.Links.Get(x => x.Code == link).Result.File);
                return file != null ? file : throw new Exception();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
