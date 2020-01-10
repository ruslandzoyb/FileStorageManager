using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.CommonModels
{
    public class FileModelView
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual UserViewModel User { get; set; }
        public virtual TypeVievModel Type { get; set; }
        public DateTime Creation { get; set; }
        public virtual PathViewModel Path { get; set; }

        public virtual StatusViewModel Status { get; set; }
        public virtual LinkViewModel Link { get; set; }
    }
}
