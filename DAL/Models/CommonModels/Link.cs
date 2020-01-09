using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.CommonModels
{
   public class Link
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int? FileId { get; set; }
        public File File { get; set; }
    }
}
