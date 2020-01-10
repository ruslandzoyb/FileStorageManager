using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.CommonModels
{
    public class LinkViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int? FileModelViewId { get; set; }
       // public FileModelView File { get; set; }
    }
}
