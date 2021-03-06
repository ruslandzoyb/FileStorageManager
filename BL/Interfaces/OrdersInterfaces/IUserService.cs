﻿using BL.ModelsDTO.FileManageDTO;
using BL.ModelsDTO.OtherModels;
using DAL.Models.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BL.Interfaces.OrdersInterfaces
{
    public interface IUserService:ILinkService
    {
        IEnumerable<FileDTO> GetList();
        FileDTO GetFile(int? id,string user_id);
        IEnumerable<FileDTO> GetFiles(string user_id);
        bool Upload(FileUploadModel file);
        FileDownloadModel Download(int? id);
        bool Delete(int? id,string user_id);
        ChangeStatusView ChangeStatus(ChangeStatusModel model);

        IEnumerable<FileDTO> Find(Expression<Func<File, bool>> predicate);
        string InfoByFile(int? id,string user_id); 

        // Additional
        //void MakeCommon(Model model); //email,file id
        //FileDTO GetCommon(int? id);// model Files ,id -user

    }
}
