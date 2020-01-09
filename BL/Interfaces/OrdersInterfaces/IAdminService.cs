using BL.ModelsDTO.ApplicationModels;
using BL.ModelsDTO.FileManageDTO;
using BL.ModelsDTO.OtherModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.OrdersInterfaces
{
   public interface IAdminService:IUserService
    {
        UserDTO GetUser(int? id);
        ApplicationUserDTO GetUserInfo(int? id);
        IEnumerable<UserDTO> GetUsers();

        string DeleteUser(int? id);


        string FrozeUser(FrozeModel model);

        IEnumerable<TypeDTO> GetTypes();

        IEnumerable<FileDTO> GetFiles();
        IEnumerable<StatusDTO> GetStatuses();
       
        //IEnumerable<Model> GetUsers(Func<Model, bool> predicate);
        //void RemoveUser(int? id);
        //void LockUser(int? id);
        //void UnlockUser(int? id);

        //IEnumerable<Model> GetFiles(Func<Model, bool> predicate);
    }
}
