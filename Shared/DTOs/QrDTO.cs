using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.DTOs
{
    public class QrDTO
    {
        public string SerialNumber { get; set; }  // Changed to PascalCase
        public string SelectedOption { get; set; } // Changed to PascalCase
        public string Customer { get; set; }
        public byte[] QRData { get; set; }
    }

}
