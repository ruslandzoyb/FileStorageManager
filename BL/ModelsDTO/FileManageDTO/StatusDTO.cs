using System;
using System.Collections.Generic;
using System.Text;

namespace BL.ModelsDTO.FileManageDTO
{
   public class StatusDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<FileDTO> Files { get; set; }
    }
}
