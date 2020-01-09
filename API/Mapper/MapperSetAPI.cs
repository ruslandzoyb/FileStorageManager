using API.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mapper
{
    public class MapperSetAPI:Profile
    {
        public MapperSetAPI()
        {
            CreateMap<ApplicationUserView, BL.ModelsDTO.ApplicationModels
                .ApplicationUserDTO>().ReverseMap();

            CreateMap<LoginModelView, BL.ModelsDTO.ApplicationModels.LoginModelDTO>().ReverseMap();

            CreateMap<DeleteAccModelView, BL.ModelsDTO.OtherModels.DeleteAccModel>().ReverseMap();
            CreateMap<UploadFileViewModel, BL.ModelsDTO.OtherModels.FileUploadModel>().ReverseMap();
            CreateMap<RoleViewModel, BL.ModelsDTO.OtherModels.RoleModel>().ReverseMap();
        }

    }
}
