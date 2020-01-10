using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.CommonModels
{
    public class DownloadViewModel
    {
        public byte[] Array { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
