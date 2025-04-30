using MES.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models
{
    public class RotorsStyle: BaseEntity
    {
        [Required(ErrorMessage = "Rotors Style is required")]
        public string? RotorsStyleName { get; set; }
        [Required(ErrorMessage = "Rotors Style Description is required")]
        public string? Description { get; set; }
    
    }
}

