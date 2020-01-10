using API.Models;
using API.Models.CommonModels;
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

            CreateMap<DownloadViewModel, BL.ModelsDTO.OtherModels.FileDownloadModel>().ReverseMap();

            CreateMap<LinkViewModel, BL.ModelsDTO.FileManageDTO.LinkDTO>().ReverseMap();
            CreateMap<PathViewModel, BL.ModelsDTO.FileManageDTO.PathDTO>().ReverseMap();
            CreateMap<TypeVievModel, BL.ModelsDTO.FileManageDTO.TypeDTO>().ReverseMap();
            CreateMap<StatusViewModel, BL.ModelsDTO.FileManageDTO.StatusDTO>().ReverseMap();
            CreateMap<UserViewModel, BL.ModelsDTO.FileManageDTO.UserDTO>().ReverseMap();
            CreateMap<FileModelView, BL.ModelsDTO.FileManageDTO.FileDTO>().ReverseMap();
        }

    }
}
