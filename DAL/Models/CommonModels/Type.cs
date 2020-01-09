using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.CommonModels
{
   public class Type
    {
        public int Id { get; set; }
        public string Format { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
