using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.ModelsDTO.OtherModels
{
   public class FileUploadModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile File { get; set; }

    }
}
