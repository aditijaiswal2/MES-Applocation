using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.DTOs
{
    public class PartInfoDTO
    {
        public string PartNumber { get; set; }
        public string BomDesc { get; set; }
        public string? CsiPartImage { get; set; }
    }
}
