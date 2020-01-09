using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.CommonModels
{
   public class Path
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int? FileId { get; set; }
        public File File { get; set; }
    }
}
