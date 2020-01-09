using System;
using System.Collections.Generic;
using System.Text;

namespace BL.ModelsDTO.OtherModels
{
   public class ChangeStatusModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string ChangedStatus { get; set; }
    }
}
