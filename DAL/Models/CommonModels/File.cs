using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.CommonModels
{
  public  class File
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Type Type { get; set; }
        
        public virtual Path Path { get; set; }
        
        public virtual Status Status { get; set; }
        public virtual Link Link { get; set; }

        
        public virtual User User { get; set; }
        public DateTime Creation { get; set; }
    }
}
