using System;
using System.Collections.Generic;
using System.Text;

namespace BL.ModelsDTO.ApplicationModels
{
   public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public IList<string> Roles { get; set; }
        public ApplicationUserDTO()
        {
            Roles = new List<string>();
        }
    }
}
