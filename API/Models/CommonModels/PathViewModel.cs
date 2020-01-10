using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.CommonModels
{
    public class PathViewModel
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int? FileModelViewId { get; set; }
       // public FileModelView File { get; set; }
    }
}
