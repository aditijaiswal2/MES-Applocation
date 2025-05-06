using MES.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models
{
   public class Other: BaseEntity
    {
        [Required(ErrorMessage = "Other is required")]
        public string? OtherName { get; set; }
        [Required(ErrorMessage = "Other Description is required")]
        public string? Description { get; set; }
    }
}

