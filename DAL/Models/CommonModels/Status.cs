using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.CommonModels
{
    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
