using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class UploadFileViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IFormFile File { get; set; }


        

    }
}
