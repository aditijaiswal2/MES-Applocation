using MES.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models
{
    public class Typesdetails : BaseEntity
    {
        [Required(ErrorMessage = "Type is required")]
        public string? TypeName { get; set; }
        [Required(ErrorMessage = "Type Description is required")]
        public string? Description { get; set; }
    }
}

