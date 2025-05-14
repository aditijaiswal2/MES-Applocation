using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models
{
     public class NewBoxRequiredNumber : BaseEntity
    {
        [Required(ErrorMessage = "New Box Required Number is required")]
        public string? NewBoxRequiredNumberName { get; set; }
        [Required(ErrorMessage = "New Box Required Number Description is required")]
        public string? Description { get; set; }
    }
}
