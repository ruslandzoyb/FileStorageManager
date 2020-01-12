using BL.ModelsDTO.ApplicationModels;
using BL.ModelsDTO.FileManageDTO;
using BL.ModelsDTO.OtherModels;
using DAL.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces.OrdersInterfaces
{
   public interface IAdminService
    {
        UserDTO GetUser(string id);
        ApplicationUserDTO GetUserInfo(string id);
        IEnumerable<UserDTO> GetUsers();
       Task<List<ApplicationUser>> GetIdentityUsers();
        string DeleteUser(string id);


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
