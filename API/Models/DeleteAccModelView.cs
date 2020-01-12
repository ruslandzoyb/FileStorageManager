﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class DeleteAccModelView
    {
       
        public string Id { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}
