using MES.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models
{
    public class SaddlepartNumber: BaseEntity
    {
        [Required(ErrorMessage = "Saddle Part Number is required")]
        public string? SaddlePartNumberName { get; set; }
        [Required(ErrorMessage = "Saddle Part Number Description is required")]
        public string? Description { get; set; }
    }
}

