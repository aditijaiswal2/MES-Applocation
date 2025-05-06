using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models
{
     public class Materials :BaseEntity
    {
     
            [Required(ErrorMessage = "Material is required")]
            public string? MaterialName { get; set; }
            [Required(ErrorMessage = "Material Description is required")]
            public string? Description { get; set; }

        }
    }

