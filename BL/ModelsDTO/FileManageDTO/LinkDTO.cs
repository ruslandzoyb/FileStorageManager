using System;
using System.Collections.Generic;
using System.Text;

namespace BL.ModelsDTO.FileManageDTO
{
   public class LinkDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int? FileDTOId { get; set; }
        public FileDTO File { get; set; }
    }
}
