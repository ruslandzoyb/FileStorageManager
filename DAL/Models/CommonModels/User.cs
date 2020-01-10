using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.CommonModels
{
   public class User
    {
        public int IdenityId { get; set; }
       // public string FullName { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
