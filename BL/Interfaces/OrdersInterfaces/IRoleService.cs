using BL.ModelsDTO.ApplicationModels;
using BL.ModelsDTO.OtherModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces.OrdersInterfaces
{
   public interface IRoleService
    {

        bool AddRole(string role);
        IEnumerable<RoleModel> GetRoles();
        string Delete(string role);



        IEnumerable<ApplicationUserDTO> GetUsersByRole(string role);
        //IEnumerable<Model> GetServices();
        //void AddService(Model modele); // model -role
        //void RemoveRole(Model model); //by id or class
    }
}
