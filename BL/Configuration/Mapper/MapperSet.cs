using AutoMapper;
using BL.ModelsDTO.ApplicationModels;
using BL.ModelsDTO.FileManageDTO;
using DAL.Models.CommonModels;
using DAL.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Text;
using Type = DAL.Models.CommonModels.Type;

namespace BL.Configuration.Mapper
{
   public class MapperSet:Profile

    {
        public MapperSet()
        {
            CreateMap<File, FileDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Type, TypeDTO>().ReverseMap();
            CreateMap<Link, LinkDTO>().ReverseMap();
            CreateMap<Status, StatusDTO>().ReverseMap();
            CreateMap<Path, PathDTO>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();

        }
    }
}
