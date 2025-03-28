using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models
{
    public class MESWorkcenters : BaseEntity
    {
        [Required(ErrorMessage = "Workcenters is required")]
        public string? Workcenters { get; set; }

        [Required(ErrorMessage = "Workcenters Description is required")]
        public string? Description { get; set; }

    }
}
