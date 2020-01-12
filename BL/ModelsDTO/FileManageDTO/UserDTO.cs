using System;
using System.Collections.Generic;
using System.Text;

namespace BL.ModelsDTO.FileManageDTO
{
  public  class UserDTO
    {
        public string IdenityId { get; set; }
       
        public virtual ICollection<FileDTO> Files { get; set; }
    }
}
