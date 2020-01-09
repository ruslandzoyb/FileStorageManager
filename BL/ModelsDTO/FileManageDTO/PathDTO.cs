using System;
using System.Collections.Generic;
using System.Text;

namespace BL.ModelsDTO.FileManageDTO
{
  public  class PathDTO
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int? FileDTOId { get; set; }
        public FileDTO File { get; set; }
    }
}
