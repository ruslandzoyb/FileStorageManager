using BL.ModelsDTO.FileManageDTO;
using BL.ModelsDTO.OtherModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces.OrdersInterfaces
{
   public interface ILinkService
    {
        FileDTO GetPath(string link);// todo:string - Path
        FileDownloadModel Download(string link);
    }
}
