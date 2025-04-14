using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.DTOs
{
    public class QRRequestDTO
    {
        public string SerialNumber { get; set; }
        public string Module { get; set; }
        public string Customer { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
