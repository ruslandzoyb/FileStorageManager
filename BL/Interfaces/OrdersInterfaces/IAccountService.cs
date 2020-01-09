using BL.ModelsDTO.ApplicationModels;
using BL.ModelsDTO.OtherModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces.OrdersInterfaces
{
    public interface IAccountService
    {
        bool CreateUser(ApplicationUserDTO user);
        string Login(LoginModelDTO model);
        

        string DeleteAccount(DeleteAccModel model);

    }
}
