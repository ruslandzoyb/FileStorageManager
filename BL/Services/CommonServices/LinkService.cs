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
using Exeptions.CE;
using BL.Configuration.FileManaging;

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

            var id = database.Links.Get(x => x.Code == link).Result.Id;
            var file = mapper.Map<FileDTO>(database.Files.Get(x => x.Link.Id == id).Result);
            if (file!=null)
            {
                return FileManagment.DownloadFile(file);
            }
            throw new LinkServiceException("File is null");
            
           
        }


        public FileDTO GetPath(string link)
        {
            if (link!=null)
            {
                var id = database.Links.Get(x => x.Code == link).Result.Id;
                var file = mapper.Map<FileDTO>(database.Files.Get(id).Result);
                if (file!=null)
                {
                    return file != null ? file : throw new Exception();
                }
                throw new LinkServiceException("File is null");
                
            }
            else
            {
                throw new LinkServiceException("Link is null");
            }
        }
    }
}
