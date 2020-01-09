using BL.ModelsDTO.FileManageDTO;
using BL.ModelsDTO.OtherModels;
using DAL.Models.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BL.Interfaces.OrdersInterfaces
{
    public interface IUserService
    {
        FileDTO GetFile(int? id);
        IEnumerable<FileDTO> GetFiles(int? user_id);
        bool Upload(FileDownloadModel file);

        bool Delete(int? id);
        ChangeStatusView ChangeStatus(ChangeStatusModel model);// id,status;

        IEnumerable<FileDTO> Find(Expression<Func<File, bool>> predicate);
        string InfoByFile(int? id); // model -file info , id -file id

        // Additional
        //void MakeCommon(Model model); //email,file id
        //FileDTO GetCommon(int? id);// model Files ,id -user

    }
}
