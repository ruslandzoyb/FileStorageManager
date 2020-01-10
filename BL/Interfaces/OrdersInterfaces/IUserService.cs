using BL.ModelsDTO.FileManageDTO;
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
        FileDTO GetFile(int? id,int user_id);
        IEnumerable<FileDTO> GetFiles(string user_id);
        string Upload(FileUploadModel file);

        bool Delete(int? id,string user_id);
        ChangeStatusView ChangeStatus(ChangeStatusModel model);// id,status;

        IEnumerable<FileDTO> Find(Expression<Func<File, bool>> predicate);
        string InfoByFile(int? id,int user_id); // model -file info , id -file id

        // Additional
        //void MakeCommon(Model model); //email,file id
        //FileDTO GetCommon(int? id);// model Files ,id -user

    }
}
