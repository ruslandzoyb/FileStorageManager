using System;
using System.Collections.Generic;
using System.Text;

namespace BL.ModelsDTO.FileManageDTO
{
  public  class FileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual UserDTO User { get; set; }
        public virtual TypeDTO Type { get; set; }
        public DateTime Creation { get; set; }
        public virtual PathDTO Path { get; set; }

        public virtual StatusDTO Status { get; set; }
        public virtual LinkDTO Link { get; set; }

       
    }
}
