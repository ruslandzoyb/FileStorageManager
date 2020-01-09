using System;
using System.Collections.Generic;
using System.Text;

namespace BL.ModelsDTO.ApplicationModels
{
   public class ApplicationUserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public IList<string> Roles { get; set; }
        public ApplicationUserDTO()
        {
            Roles = new List<string>();
        }
    }
}
