using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.DTOs
{
   public class RotorsStyleQRDTO
    {
        public string RotorsStyleName { get; set; }
        public string Description { get; set; }


        public byte[] QRData { get; set; }
    }
}
