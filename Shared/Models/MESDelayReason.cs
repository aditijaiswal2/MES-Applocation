using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models
{
    public class MESDelayReason : BaseEntity
    {
        [Required(ErrorMessage = "DelayReason is required")]
        public string? DelayReason { get; set; }

        [Required(ErrorMessage = "DelayReason Description is required")]
        public string? Description { get; set; }

    }
}
