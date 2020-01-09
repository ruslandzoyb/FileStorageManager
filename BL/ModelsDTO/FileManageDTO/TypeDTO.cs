using System;
using System.Collections.Generic;
using System.Text;

namespace BL.ModelsDTO.FileManageDTO
{
   public class TypeDTO
    {
        public int Id { get; set; }
        public string Format { get; set; }
        public virtual ICollection<FileDTO> Files { get; set; }
    }
}
